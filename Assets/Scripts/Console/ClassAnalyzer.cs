using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Assets.Scripts.Console.Attributes;
using Assets.Scripts.Console.Exceptions;
using Assets.Scripts.Console.Parameters;
using UnityEngine;

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
            {typeof(bool), typeof(BoolParameter) },
            {typeof(byte), typeof(ByteParamter) },
            {typeof(char), typeof(CharParameter) },
            {typeof(decimal), typeof(DecimalParameter) },
            {typeof(double), typeof(DoubleParameter) },
            {typeof(float), typeof(FloatParameter) },
            {typeof(long), typeof(LongParameter) },
            {typeof(sbyte), typeof(SbyteParameter) },
            {typeof(short), typeof(ShortParameter) },
            {typeof(uint), typeof(UintParameter) },
            {typeof(ulong), typeof(UlongParameter) },
            {typeof(ushort), typeof(UshortParameter) },
            {typeof(string), typeof(StringParameter) },
            {typeof(Vector2), typeof(Vectro2Parameter) },
            {typeof(Vector3), typeof(Vector3Parameter) },
            {typeof(Color), typeof(ColorParameter) },
        };

        public static List<IConsoleCommand> GetCommands(Type t, object obj, ImportType importType)
        {
            var className = t.Name;
            var prefix = className + ".";
            var commands = new List<IConsoleCommand>();

            foreach (var methodInfo in t.GetMethods())
            {
                //Filter out the object Methods
                if(methodInfo.Name == "Equals" || methodInfo.Name == "GetHashCode" || methodInfo.Name == "GetType" || methodInfo.Name == "ToString") continue;

                //Check if it is private
                if (importType == ImportType.PublicOnly && methodInfo.IsPrivate) continue;

                var attributes = methodInfo.GetCustomAttributes(typeof(ConsoleCommandAttribute), true);
                //Check for ConsoleCommandAttribite
                if (importType == ImportType.Marked && attributes.Length == 0) continue;

                var commandName = "";
                if (attributes.Length > 0)
                {
                    var cca = (ConsoleCommandAttribute) attributes[0];
                    if (cca.Global) prefix = "";
                    if (!string.IsNullOrEmpty(cca.Name))
                    {
                        commandName = prefix + cca.Name;
                    }
                    else
                    {
                        commandName = prefix + methodInfo.Name;
                    }
                }
                else
                {
                    commandName = prefix + methodInfo.Name;
                }

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
            var attribute = pInfo.GetCustomAttributes(typeof(ConsoleParameterAttribute), true).FirstOrDefault();

            Type t = null;
            string name = null;
            ConsoleNumericParameterAttribute cnpa = null;
            if (attribute != null)
            {
                var cpa = (ConsoleParameterAttribute)attribute;
                t = cpa.ParameterType;
                name = cpa.ParameterName;
                if (cpa is ConsoleNumericParameterAttribute)
                {
                    cnpa = (ConsoleNumericParameterAttribute)cpa;
                }
            }

            if (!DefaultParameters.ContainsKey(pInfo.ParameterType))
            {
                throw new ConsoleException(string.Format("Cannot find a parameter for the type {0}", pInfo.ParameterType));
            }
            if (t == null) t = DefaultParameters[pInfo.ParameterType];

            object o = t.Assembly.CreateInstance
            (
                typeName: t.FullName,
                ignoreCase: true,
                bindingAttr: BindingFlags.ExactBinding,
                binder: null,
                args: new object[]
                {
                    name ?? pInfo.Name,
                    pInfo.IsOptional
                },
                culture: CultureInfo.CurrentCulture,
                activationAttributes: null
            );

            if (cnpa != null)
            {
                var m = t.GetMethod("SetRange", new[] { pInfo.ParameterType, pInfo.ParameterType });
                var from = Convert.ChangeType(cnpa.From, pInfo.ParameterType);
                var till = Convert.ChangeType(cnpa.Till, pInfo.ParameterType);

                m.Invoke(o, new[] { from, till });
            }

            return (IParameter)o;
        }
    }
}
