using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Console.Exceptions;
using Assets.Scripts.Console.Parameters;

namespace Assets.Scripts.Console
{
    public class ConsoleCommand : IConsoleCommand
    {
        public delegate void ConsoleAction(object[] arguments);
        
        public ConsoleAction Action { get; private set; }
        public string CommandName { get; private set; }
        public IParameter[] Parameters { get; private set; }

        public ConsoleCommand(string name, ConsoleAction action)
        {
            CommandName = name;
            Action = action;
            Parameters = new IParameter[0];
        }

        public ConsoleCommand(string name, ConsoleAction action, IParameter[] parameters) : this(name, action)
        {
            Parameters = parameters;
        }

        public void Execute(params string[] arguments)
        {
            if(arguments == null) arguments = new string[0];

            if (arguments.Length > Parameters.Length)
            {
                throw new CommandException("To many Arguments for this command", this);
            }

            var args = new List<object>();

            for (int i = 0; i < arguments.Length; i++)
            {
                args.Add(Parameters[i].Parse(arguments[i]));
            }

            foreach (var parameter in Parameters.Skip(arguments.Length))
            {
                if (!parameter.Optional)
                {
                    throw new CommandException(string.Format("Parameter {0} is required", parameter.Name), this);
                }
            }

            Action.Invoke(args.ToArray());
        }
    }
}
