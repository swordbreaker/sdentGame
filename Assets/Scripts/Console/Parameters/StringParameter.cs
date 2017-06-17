using System;

namespace Assets.Scripts.Console.Parameters
{
    public class StringParameter : Parameter
    {
        public StringParameter(string name, bool optional = false) : base(name, optional)
        {
            
        }

        protected override object ParseValue(string s)
        {
            return s;
        }

        public override bool CanParse(string value)
        {
            return true;
        }

        public override void Validate(string value)
        {
        }

        public override Type GetParamType()
        {
            return typeof(string);
        }
    }
}
