using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sprache;

namespace Assets.Scripts.Console.Parameters
{
    public class IListParameter<T> : AbstractClassParameter<IList<T>>
    {
        public IListParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        public override Type GetParamType()
        {
            return typeof(List<T>);
        }

        public override string GetSyntax()
        {
            return string.Format("{{{0}:e1 {0}:e2 ... {0}:en}}", typeof(T).Name);
        }

        protected override Parser<IList<T>> Parser
        {
            get
            {
                var parType = Console.DefaultParameters[typeof(T)];
                var parameter = ClassAnalyzer.ConstructParameter(parType, "element", false);

                return from first in Sprache.Parse.Char('{')
                    from elements in Sprache.Parse.AnyChar.Many().Token().Many()
                    from last in Sprache.Parse.Char('}')
                    select new List<T>(elements.Select(s => (T)parameter.Parse(new string(s.ToArray()))));
            }
        }
    }
}
