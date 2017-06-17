using System.Linq;
using Sprache;

namespace Assets.Scripts.Console.Parameters
{
    public class ArrayParameter<T> : IListParameter<T>
    {
        public ArrayParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        protected override object ParseValue(string s)
        {
            return Parser.Parse(s).ToArray();
        }
    }
}
