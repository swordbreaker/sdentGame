using System;
using Assets.Scripts.Console;

namespace Assets.Scripts
{
    public class ConsoleParameterAttribute : Attribute
    {
        public Type T;
        public Type ParameterType { get; set; }
        public string ParameterName { get; set; }
        public Range Range { get; set; }

        public ConsoleParameterAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }
    }
}
