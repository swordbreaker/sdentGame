using System;

namespace Assets.Scripts.Console.Parameters
{
    public class BoolParameter : Parameter
    {
 
        public BoolParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        protected override object ParseValue(string s)
        {
            return bool.Parse(s);
        }

        public override bool CanParse(string value)
        {
            bool tmp;
            return bool.TryParse(value, out tmp);
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
