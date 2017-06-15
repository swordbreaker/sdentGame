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
        public enum ImportType
        {
            All, PublicOnly, Marked
        }

        private static readonly Dictionary<Type, Type> DefaultParameters = new Dictionary<Type, Type>()
        {
            {typeof(float), typeof(FloatParameter) },
            {typeof(double), typeof(DoubleParameter) },
            {typeof(string), typeof(StringParameter) }
        };

        public static List<IConsoleCommand> GetCommands(Type t, object obj, ImportType importType)
        {
            var className = t.Name;
            var commands = new List<IConsoleCommand>();

            foreach (var methodInfo in t.GetMethods())
            {
                //Check if it is private
                if(importType == ImportType.PublicOnly && methodInfo.IsPrivate) continue;

                //Check for ConsoleCommandAttribite
                if (importType == ImportType.Marked && 
                    methodInfo
                    .GetCustomAttributes(true)
                    .All(o => o.GetType() != typeof(ConsoleCommandAttribute))
                    )
                    continue;
                
                var commandName = className + "." + methodInfo.Name;
                var parameters = methodInfo.GetParameters().Select(ConstructParameter).ToList();

                var mInfo = methodInfo;
                commands.Add(
                    new ConsoleCommand(commandName, arguments => mInfo.Invoke(obj, arguments), parameters.ToArray())
                    );                
            }

            return commands;
        }

        private static IParameter ConstructParameter(ParameterInfo pInfo)
        {
            var t = DefaultParameters[pInfo.ParameterType];
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
