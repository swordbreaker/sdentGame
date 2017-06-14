using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Assets.Scripts.Console.Parameters;

namespace Assets.Scripts.Console
{
    public static class ClassAnalyzer
    {
        private static Dictionary<Type, Type> _defaultParameters = new Dictionary<Type, Type>()
        {
            {typeof(float), typeof(FloatParameter) },
            {typeof(double), typeof(DoubleParameter) },
            {typeof(string), typeof(StringParameter) }
        };

        public static List<IConsoleCommand> GetCommands(Type t, object obj)
        {
            var className = t.Name;
            var commands = new List<IConsoleCommand>();

            foreach (var methodInfo in t.GetMethods())
            {
                if (methodInfo.GetCustomAttributes(true).Any(o => o.GetType() == typeof(ConsoleCommandAttribute)))
                {
                    var commandName = className + "." + methodInfo.Name;
                    var parameters = methodInfo.GetParameters().Select(ConstructParameter).ToList();

                    var mInfo = methodInfo;
                    commands.Add(new ConsoleCommand(commandName, arguments => mInfo.Invoke(obj, arguments), parameters.ToArray()));
                }
            }

            return commands;
        }

        private static IParameter ConstructParameter(ParameterInfo pInfo)
        {
            var t = _defaultParameters[pInfo.ParameterType];
            var o = t.Assembly.CreateInstance
            (
                typeName: t.FullName,
                ignoreCase: true,
                bindingAttr: BindingFlags.ExactBinding,
                binder: null,
                args: new object[]
                {
                    pInfo.Name,
                    pInfo.IsOptional
                },
                culture: CultureInfo.CurrentCulture,
                activationAttributes: null
            );

            return (IParameter) o;
        }
    }
}
