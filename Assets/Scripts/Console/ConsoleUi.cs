using System.Linq;
using Assets.Scripts.Console.Parameters;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Console
{
    public class ConsoleUi : MonoBehaviour
    {
        [SerializeField] private InputField _inputField;
        [SerializeField] private Text _textField;
        [SerializeField] private RectTransform _contentField;
        [SerializeField] private ScrollRect _scrollRect;

        private float _startHeight;
        private bool _lockTab;

        private void Start()
        {
            Console.Instance.RegisterCommand(new ConsoleCommand("help", arguments => Console.Instance.Help()));
            Console.Instance.RegisterCommand(new ConsoleCommand("test", arguments => print("test" + arguments[0] + " " + arguments[1]), new IParameter[] { new FloatParameter("par1"), new StringParameter("par2") }));
            Console.Instance.RegisterCommand(new ConsoleCommand("cls", arguments => Clear(), ""));
            var testClass = new TestClass();
            Console.Instance.RegisterClass<TestClass>(testClass);

            _startHeight = _contentField.rect.height;
            Console.Instance.OnLog += (sender, args) => Log(args.Message);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
            {
                Console.Instance.HistoryManager.AddToHistory(_inputField.text);
                Console.Instance.Execute(_inputField.text);
                _inputField.text = "";
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _inputField.text = Console.Instance.HistoryManager.Up();
                _inputField.MoveTextEnd(false);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _inputField.text = Console.Instance.HistoryManager.Down();
                _inputField.MoveTextEnd(false);
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
            if (_lockTab && Event.current.type == EventType.KeyUp) _lockTab = false;

            if ((Event.current.keyCode == KeyCode.Tab || Event.current.character == '\t') && Event.current.type == EventType.KeyDown && !_lockTab)
            {
                Event.current.Use();
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
                }
            }

            if (Event.current.keyCode == KeyCode.Return)
            {
                Event.current.Use();
            }
        }

        private void Clear()
        {
            _textField.text = "";
        }
    }
}
