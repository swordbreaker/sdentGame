using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace Assets.Scripts.Console.Parameters
{
    public abstract class AbstractClassParameter<T> : Parameter
    {
        protected AbstractClassParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        protected override object ParseValue(string s)
        {
            return Parser.Parse(s);
        }

        public override bool CanParse(string value)
        {
            var p = Parser.TryParse(value);
            return p.WasSuccessful;
        }

        public override string GetSyntax()
        {
            return string.Format("[x y]:{0}", Name);
        }

        protected abstract Parser<T> Parser { get; }
}
}
