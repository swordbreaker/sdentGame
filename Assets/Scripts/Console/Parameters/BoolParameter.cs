using System;

namespace Assets.Scripts.Console.Parameters
{
    public class BoolParameter : Parameter
    {
        private static readonly Predicate<string> CanParsePredicate = s =>
        {
            bool tmp;
            return bool.TryParse(s, out tmp);
        };

        public BoolParameter(string name, bool optional = false) : base(name, s => bool.Parse(s), CanParsePredicate, optional: optional)
        {
        }

        public override void Validate(string value)
        {
        }

        public override Type GetParamType()
        {
            return typeof(bool);
        }
    }
}
