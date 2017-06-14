using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity;
using UnityEngine;

namespace Assets.Scripts.Console.Parameters
{
    public class Console : MonoBehaviour
    {
        [SerializeField] private UnityEngine.UI.InputField _inputField;

        private readonly Dictionary<string, ConsoleCommand> _registeredCommands = new Dictionary<string, ConsoleCommand>();

        public void RegisterCommand(ConsoleCommand command)
        {
            _registeredCommands.Add(command.CommandName, command);
        }

        private void Start()
        {
            _inputField.onValueChanged.AddListener(InputChanged);
            _inputField.onEndEdit.AddListener(OnSubmit);
        }

        private void OnSubmit(string arg0)
        {
            var splitts = _inputField.text.Split(' ');

            if (_registeredCommands.ContainsKey(splitts[0]))
            {
                _registeredCommands[splitts[0]].Execute(splitts.Skip(1).ToArray());
            }
            else
            {
                print("Command not found");
            }
        }

        private void InputChanged(string arg0)
        {
            
        }
    }
}
