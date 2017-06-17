using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace Assets.Scripts.Console.Parameters
{
    public class ListParameter<T> : IListParameter<T>
    {
        public ListParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        protected override object ParseValue(string s)
        {
            return Parser.Parse(s).ToList();
        }
    }
}
