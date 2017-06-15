using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Console.Parameters
{
    public class LongParameter : NumericParameter<long>
    {
        public LongParameter(string name, bool optional) : base(name, long.Parse, optional)
        {
        }
    }
}
