using System;

namespace Assets.Scripts.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class ConsoleParameterAttribute : Attribute
    {
        public Type T;
        public Type ParameterType { get; set; }
        public string ParameterName { get; set; }
    }
}
