using UnityEngine;
using System.Collections;
using System.Linq;
using Assets.Scripts.Movement;

public class ConsoleGUI : MonoBehaviour
{
    public ConsoleAction escapeAction;
    public ConsoleAction submitAction;
    [HideInInspector]
    public string input = "";
    private ConsoleLog consoleLog;
    private Rect consoleRect;
    private bool focus = false;
    private const int WINDOW_ID = 50;

    private ConsoleCommandsRepository consoleCommandsRepository;

    private MoveController _moveController;
    private Vector2 scrollPosition;
    private bool _lockTab;

    private MoveController MoveController
    {
        get
        {
            if (_moveController == null)
            {
                _moveController = FindObjectOfType<MoveController>();
            }
            return _moveController;
        }
    }

    private void Start()
    {
        consoleRect = new Rect(0, 0, Screen.width, Mathf.Min(300, Screen.height));
        consoleLog = ConsoleLog.Instance;
        consoleCommandsRepository = ConsoleCommandsRepository.Instance;
    }

    private void OnEnable()
    {
        MoveController.MouseLook.SetCursorLock(false);
        focus = true;
    }

    private void OnDisable()
    {
        MoveController.MouseLook.SetCursorLock(true);
        focus = true;
    }

    public void OnGUI()
    {
        GUILayout.Window(WINDOW_ID, consoleRect, RenderWindow, "Console");
    }

    private void RenderWindow(int id)
    {
        HandleSubmit();
        HandleEscape();
        HandleTab();

        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
        GUILayout.Label(consoleLog.log);
        GUILayout.EndScrollView();
        GUI.SetNextControlName("input");
        input = GUILayout.TextField(input);
        if (focus)
        {
            GUI.FocusControl("input");
            focus = false;
        }
    }

    private void HandleSubmit()
    {
        if (KeyDown("[enter]") || KeyDown("return"))
        {
            if (submitAction != null)
            {
                submitAction.Activate();
                scrollPosition.y = Mathf.Infinity;
            }
            input = "";
        }
    }

    private void HandleEscape()
    {
        if (KeyDown("escape") || KeyDown("F12"))
        {
            escapeAction.Activate();
            input = "";
        }
    }

    private bool KeyDown(string key)
    {
        return Event.current.Equals(Event.KeyboardEvent(key));
    }

    private void HandleTab()
    {
        if (_lockTab && Event.current.type == EventType.KeyUp) _lockTab = false;

        if (Event.current.keyCode == KeyCode.Tab && Event.current.type == EventType.KeyDown && !_lockTab)
        {
            _lockTab = true;
            var commands = consoleCommandsRepository.Repository.Keys.Where(s => s.Contains(input)).ToList();
            scrollPosition.y = Mathf.Infinity;

            if (commands.Count == 1)
            {
                input = commands[0];
            }
            else
            {
                commands.ForEach(s => consoleLog.Log("> " + s));
            }
        }
    }
}
