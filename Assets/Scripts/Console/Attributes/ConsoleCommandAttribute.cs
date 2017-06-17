using System;

namespace Assets.Scripts.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ConsoleCommandAttribute : Attribute
    {
        public string Name { get; set; }
        public bool Global { get; set; }
    }
}
