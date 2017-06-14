using System;

namespace Assets.Scripts.Console.Parameters
{
    public class StringParameter : Parameter
    {
        public StringParameter(string name, bool optional = false) : base(name, s => s, s => true, optional: optional)
        {
            
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
