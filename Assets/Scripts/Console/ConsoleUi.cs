using System.Collections;
using System.Linq;
using Assets.Scripts.Console.Parameters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.Console
{
    public class ConsoleUi : MonoBehaviour
    {
        [SerializeField] private GameObject _consolePanel;
        [SerializeField] private InputField _inputField;
        [SerializeField] private Text _textField;
        [SerializeField] private RectTransform _contentField;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private KeyCode _openCloseConsoleKey = KeyCode.F6;
        [SerializeField] private KeyCode _completeCommandKeyCode = KeyCode.Tab;
        [SerializeField] private KeyCode _historyNextKeyCode = KeyCode.UpArrow;
        [SerializeField] private KeyCode _historyPreviousKeyCode = KeyCode.DownArrow;

        private RectTransform _panelRectTransform;
        private float _startHeight;
        private bool _lockTab;
        private bool _isActive;
        private float _panelHeight;
        private Vector3 _activePos;
        private Vector3 _inActivePos;

        private void Start()
        {
            Console.Instance.RegisterCommand(new ConsoleCommand("help", arguments => Console.Instance.Help()));
            Console.Instance.RegisterCommand(new ConsoleCommand("test", arguments => print("test" + arguments[0] + " " + arguments[1]), new IParameter[] { new FloatParameter("par1"), new StringParameter("par2") }));
            Console.Instance.RegisterCommand(new ConsoleCommand("cls", arguments => Clear(), ""));
            var testClass = new TestClass();
            Console.Instance.RegisterClass<TestClass>(testClass, ClassAnalyzer.ImportType.PublicOnly);

            _startHeight = _contentField.rect.height;
            Console.Instance.OnLog += (sender, args) => Log(args.Message);

            _isActive = false;
            _panelRectTransform = _consolePanel.GetComponent<RectTransform>();
            _activePos = _panelRectTransform.position;
            _panelHeight = _panelRectTransform.rect.height;
            _inActivePos = new Vector3(_panelRectTransform.position.x, _panelRectTransform.position.y + _panelHeight, _panelRectTransform.position.z);
            _panelRectTransform.position = _inActivePos;
            _consolePanel.SetActive(false);
        }

        private void Update()
        {
            if (_isActive)
            {
                if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    Console.Instance.HistoryManager.AddToHistory(_inputField.text);
                    Console.Instance.Execute(_inputField.text);
                    _inputField.text = "";
                }

                if (Input.GetKeyDown(_historyNextKeyCode))
                {
                    _inputField.text = Console.Instance.HistoryManager.Up();
                    _inputField.MoveTextEnd(false);
                }

                if (Input.GetKeyDown(_historyPreviousKeyCode))
                {
                    _inputField.text = Console.Instance.HistoryManager.Down();
                    _inputField.MoveTextEnd(false);
                }

                if (Input.GetKeyDown(_openCloseConsoleKey))
                {
                    StartCoroutine(Deactivate());
                }

                if (Input.GetKeyDown(_completeCommandKeyCode))
                {
                    var matchingCommands = Console.Instance.GetMatchingCommands(_inputField.text);
                    if (matchingCommands.Count == 1)
                    {
                        _inputField.text = matchingCommands.First();
                        _inputField.MoveTextEnd(false);
                    }
                    else
                    {
                        foreach (var cmd in matchingCommands)
                        {
                            Log(cmd);
                        }
                        Log(" ");
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(_openCloseConsoleKey))
                {
                    StartCoroutine(Acitvate());
                }
            }
        }

        private IEnumerator Acitvate()
        {
            if (!_isActive)
            {
                _isActive = true;
                Console.Instance.IsAcitve = true;
                _consolePanel.SetActive(true);
                //Animate Panel appearance
                yield return StartCoroutine(Move(_panelRectTransform, _activePos, 0.5f));
                EventSystem.current.SetSelectedGameObject(_inputField.gameObject, null);
            }
        }

        private IEnumerator Deactivate()
        {
            if (_isActive)
            {
                _isActive = false;
                Console.Instance.IsAcitve = false;
                //Animate Panel appearance
                yield return StartCoroutine(Move(_panelRectTransform, _inActivePos, 0.5f));
                _consolePanel.SetActive(false);
            }
        }

        private IEnumerator Move(Transform transform, Vector3 to, float travelTime)
        {
            var startTime = Time.time;
            var startPos = transform.position;

            var t = 0f;
            while (t < 1f)
            {
                t = (Time.time - startTime) / travelTime;
                transform.position = Vector3.Lerp(startPos, to, t);
                yield return new WaitForEndOfFrame();
            }
        }

        private void Log(string message)
        {
            _textField.text += message + "\n";
            if (_textField.preferredHeight > _startHeight)
            {
                //Extend the scroll content
                _contentField.sizeDelta = new Vector2(_contentField.sizeDelta.x, _textField.preferredHeight);
                //Scroll to bottom
                _scrollRect.verticalNormalizedPosition = 0f;
            }
        }

        private void InputChanged(string arg0)
        {

        }

        private void OnGUI()
        {
            if(!_isActive) return;

            if (Event.current.keyCode == _completeCommandKeyCode)
            {
                Event.current.Use();
            }

            if (Event.current.keyCode == KeyCode.Return)
            {
                Event.current.Use();
            }
        }

        private void Clear()
        {
            _textField.text = "";
            _contentField.sizeDelta = new Vector2(_contentField.sizeDelta.x, _startHeight);
        }
    }
}