using System;

namespace Assets.Scripts.Console.Parameters
{
    public class UlongParameter : NumericParameter<ulong>
    {
        public UlongParameter(string name, bool optional) : base(name, ulong.Parse, optional)
        {
        }
    }
}
