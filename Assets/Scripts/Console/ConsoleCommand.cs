using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.Console.Exceptions;
using Assets.Scripts.Console.Parameters;

namespace Assets.Scripts.Console
{
    public class ConsoleCommand
    {
        public delegate object ConsoleAction(params object[] arguments);
        
        public ConsoleAction Action { get; set; }
        public string CommandName { get; set; }
        public IParameter[] Parameters { get; set; }

        public void Execute(params string[] arguments)
        {
            if (arguments.Length > Parameters.Length)
            {
                throw new CommandException("To many Arguments for this command", this);
            }

            var args = new List<object>();

            for (int i = 0; i < arguments.Length; i++)
            {
                args.Add(Parameters[i].Parse(arguments[i]));
            }
            if (Parameters.Skip(arguments.Length).Any(parameter => !parameter.Optional))
            {
                //TODO params required 
            }

            Action.Invoke(args);
        }
    }
}
