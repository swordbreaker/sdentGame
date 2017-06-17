using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Console.Attributes;
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

        [ConsoleCommand]
        public static void StaticTest()
        {
            Debug.Log("static");
        }

        [ConsoleCommand]
        public void Vector2Test(Vector2 v2)
        {
            Debug.Log(v2);
        }

        [ConsoleCommand]
        public void Vector3Test(Vector3 v3)
        {
            Debug.Log(v3);
        }

        [ConsoleCommand]
        public void RangeTest([ConsoleNumericParameter(0f, 1f, ParameterName = "other Name")]float r)
        {
            
        }

        [ConsoleCommand(Name = "Hambbe", Global = true)]
        public void ColorTest(Color c)
        {
            Debug.Log(c);
        }
    }

    public class Console
    {
        private readonly Dictionary<string, IConsoleCommand> _registeredCommands = new Dictionary<string, IConsoleCommand>();
        private readonly AutoCompleteManger _autoCompleteManger = new AutoCompleteManger();

        private static Console _instance;
        public readonly ConsoleHistoryManager HistoryManager = new ConsoleHistoryManager();

        private readonly Color _infoColor = Color.black;
        private readonly Color _warningColor = Color.yellow;
        private readonly Color _errorColor = Color.red;
        private bool _isAcitve;


        public bool IsAcitve
        {
            get { return _isAcitve; }
            set
            {
                _isAcitve = value;
                if(_isAcitve)
                {
                    if (OnActivate != null) OnActivate.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    if (OnDeActivate != null) OnDeActivate.Invoke(this, EventArgs.Empty);
                }
            }
        }

        public enum LogType
        {
            Info,
            Warning,
            Error
        }

        public class ConsoleLogEventArgs : EventArgs
        {
            public string Message { get; set; }

            public ConsoleLogEventArgs(string message)
            {
                Message = message;
            }
        }

        public event EventHandler<ConsoleLogEventArgs> OnLog;
        public event EventHandler OnActivate;
        public event EventHandler OnDeActivate;

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
                _autoCompleteManger.Add(s[0] + ".", s[1]);
            }
            else
            {
                _autoCompleteManger.Add(command.CommandName);
            }
            
        }

        public void RegisterClass<T>(object instance, ClassAnalyzer.ImportType importType = ClassAnalyzer.ImportType.Marked)
        {
            foreach (var cmd in ClassAnalyzer.GetCommands(typeof(T), instance, importType))
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

            var bracketsParser =
                from leading in Parse.WhiteSpace.Many()
                from first in Parse.Chars('(', '[', '{').Once()
                from inner in Parse.AnyChar.Except(Parse.Chars(')', ']', '}').Once()).Many()
                from last in Parse.Chars(')', ']', '}').Once()
                select new string(first.Concat(inner).Concat(last).ToArray());

            var argumentWithoutQuoatParser =
                from leading in Parse.WhiteSpace.Many()
                from argumen in Parse.LetterOrDigit.Or(Parse.Char('.')).Many().Or(Parse.Char('?').Once())
                select new string(argumen.ToArray());

            var argumentParser = argumenWithQuoatParser.Or(bracketsParser).Or(argumentWithoutQuoatParser);

            var parser = from cmd in commandParser
                from w in Parse.WhiteSpace.Many()
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
                        if (string.Equals(tuple.V2.FirstOrDefault(), "?", StringComparison.InvariantCultureIgnoreCase))
                        {
                            Log(_registeredCommands[cmd].GetCommandSyntax());
                        }
                        else
                        {
                            _registeredCommands[cmd].Execute(tuple.V2.ToArray());
                            if (_registeredCommands[cmd].ReturnMessage != null)
                            {
                                Log(_registeredCommands[cmd].ReturnMessage);
                            }
                        }
                    }
                    catch (ConsoleException e)
                    {
                        Log(e.Message, LogType.Error);
                    }
                }
                else
                {
                    Log("Command not found", LogType.Error);
                }
            }
            else
            {
                Log("Cannot parse your comment", LogType.Error);
            }

        }

        public List<string> GetMatchingCommands(string pattern)
        {
            return _autoCompleteManger.GetAwaibleCommands(pattern).ToList();
        }

        public void Help()
        {
            foreach (var cmd in _registeredCommands.Keys)
            {
                Log("-" + cmd);
            }
        }

        public void Log(string msg, LogType logType = LogType.Info)
        {
            if (OnLog != null)
            {
                var color = "";
                switch (logType)
                {
                    case LogType.Info:
                        
                        color = string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGBA(_infoColor));
                        break;
                    case LogType.Warning:
                        color = string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGBA(_warningColor));
                        break;
                    case LogType.Error:
                        color = string.Format("<color=#{0}>", ColorUtility.ToHtmlStringRGBA(_errorColor));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("logType", logType, null);
                }

                OnLog(this, new ConsoleLogEventArgs(color + msg + "</color>"));
            }
        }
    }
}
