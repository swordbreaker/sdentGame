using System.Linq;
using Assets.Scripts.Console.ConsoleParser;

namespace Assets.Scripts.Console.Parameters
{
    public class ListParameter<T> : IListParameter<T>
    {
        public ListParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        protected override object ParseValue(IValue v)
        {
            return GetList(v).ToList();
        }
    }
}
