﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Assets.Scripts.Console.Exceptions;
using Sprache;

namespace Assets.Scripts.Console
{
    public class TestClass
    {
        [ConsoleCommand]
        public void PrintMe(string message)
        {
            Debug.Log(message);
        }

        [ConsoleCommand]
        public void Calc(float a, float b)
        {
            Debug.Log(a + b);
        }

        [ConsoleCommand]
        public void Ping()
        {
            Debug.Log("pong");
        }
    }

    public class Console
    {
        private readonly Dictionary<string, IConsoleCommand> _registeredCommands = new Dictionary<string, IConsoleCommand>();
        private readonly HashSet<string> _autocompleteSet = new HashSet<string>();

        private static Console _instance;
        public readonly ConsoleHistoryManager HistoryManager = new ConsoleHistoryManager();

        public class ConsoleLogEventArgs : EventArgs
        {
            public string Message { get; set; }

            public ConsoleLogEventArgs(string message)
            {
                Message = message;
            }
        }

        public event EventHandler<ConsoleLogEventArgs> OnLog;

        public static Console Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Console();
                }
                return _instance;
            }
        }

        public void RegisterCommand(IConsoleCommand command)
        {
            _registeredCommands.Add(command.CommandName, command);
            var s = command.CommandName.Split('.');
            if (s.Length > 1)
            {
                if (!_autocompleteSet.Contains(s[0] + "."))
                {
                    _autocompleteSet.Add(s[0] + ".");
                }
                _autocompleteSet.Add(s[1]);
            }
            else
            {
                _autocompleteSet.Add(command.CommandName);
            }
            
        }

        public void RegisterClass<T>(object instance)
        {
            var commands = ClassAnalyzer.GetCommands(typeof(T), instance);
            foreach (var cmd in ClassAnalyzer.GetCommands(typeof(T), instance))
            {
                RegisterCommand(cmd);
            }
        }

        public void Execute(string arg)
        {
            var simpleCommandParser = from leading in Parse.WhiteSpace.Many()
                from first in Parse.Letter.Once()
                from rest in Parse.LetterOrDigit.Many()
                select new string(first.Concat(rest).ToArray());

            var classParser = from leading in Parse.WhiteSpace.Many()
                from first in Parse.Letter.Once()
                from c in Parse.LetterOrDigit.Many()
                from point in Parse.Char('.')
                from m in Parse.LetterOrDigit.Many()
                select new string(first.Concat(c).ToArray()) + point + new string(m.ToArray());

            var commandParser = classParser.Or(simpleCommandParser);

            var argumenWithQuoatParser =
                from first in Parse.WhiteSpace.Many()
                from q in Parse.Char('"')
                from bb in Parse.AnyChar.Until(Parse.Char('"'))
                select new string(bb.ToArray());

            var argumentWithoutQuoatParser =
                from first in Parse.WhiteSpace.Many()
                from argumen in Parse.LetterOrDigit.Many()
                select new string(argumen.ToArray());

            var argumentParser = argumenWithQuoatParser.Or(argumentWithoutQuoatParser);

            var parser = from cmd in commandParser
                from par in argumentParser.Many()
                select new Tuple<string, IEnumerable<string>>(cmd, par);


            var tupleResult = parser.TryParse(arg);

            if (tupleResult.WasSuccessful)
            {
                var tuple = tupleResult.Value;

                var cmd = tuple.V1;

                if (_registeredCommands.ContainsKey(cmd))
                {
                    try
                    {
                        _registeredCommands[cmd].Execute(tuple.V2.ToArray());
                        Log("Command executed");
                    }
                    catch (ConsoleException e)
                    {
                        Log(e.Message);
                    }
                }
                else
                {
                    Log("Command not found");
                }
            }
            else
            {
                Log("Cannot parse your comment");
            }

        }

        public List<string> GetMatchingCommands(string pattern)
        {
            return _autocompleteSet.Where(s => s.Contains(pattern)).ToList();
        }

        public void Help()
        {
            foreach (var cmd in _registeredCommands.Keys)
            {
                Log("-" + cmd);
            }
        }

        private void Log(string msg)
        {
            if (OnLog != null) OnLog(this, new ConsoleLogEventArgs(msg));
        }
    }
}
