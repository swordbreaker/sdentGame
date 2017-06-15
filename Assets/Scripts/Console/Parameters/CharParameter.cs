using System;

namespace Assets.Scripts.Console.Parameters
{
    public class CharParameter : Parameter
    {
        private static readonly Predicate<string> CanParsePredicate = s =>
        {
            char tmp;
            return char.TryParse(s, out tmp);
        };

        public CharParameter(string name, bool optional = false) : base(name, s => char.Parse(s), CanParsePredicate, optional: optional)
        {
        }

        public override void Validate(string value)
        {
        }

        public override Type GetParamType()
        {
            return typeof(char);
        }
    }
}
