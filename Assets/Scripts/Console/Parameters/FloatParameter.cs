using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Assets.Scripts.Console.Exceptions;

namespace Assets.Scripts.Console.Parameters
{
    public class FloatParameter : NumericParameter<float>
    {
        public FloatParameter(string name, bool optional = false) : base(name, float.Parse, optional) {}
    }
}
