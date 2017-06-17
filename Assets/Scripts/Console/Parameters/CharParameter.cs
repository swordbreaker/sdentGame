using System;

namespace Assets.Scripts.Console.Parameters
{
    public class CharParameter : Parameter
    {

        public CharParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        protected override object ParseValue(string s)
        {
            return char.Parse(s);
        }

        public override bool CanParse(string value)
        {
            char tmp;
            return char.TryParse(value, out tmp);
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
