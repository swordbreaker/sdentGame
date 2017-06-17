using System;
using System.Linq;
using Sprache;
using UnityEngine;

namespace Assets.Scripts.Console.Parameters
{
    public class ColorParameter : AbstractClassParameter<Color>
    {
        public ColorParameter(string name, bool optional = false) : base(name, optional)
        {
        }

        public override Type GetParamType()
        {
            return typeof(Color);
        }

        public override string GetSyntax()
        {
            return string.Format("(r g b a):{0}", Name);
        }

        protected override Parser<Color> Parser
        {
            get
            {
                return from first in Sprache.Parse.Char('(')
                    from r in Sprache.Parse.Decimal.Token()
                    from g in Sprache.Parse.Decimal.Token()
                    from b in Sprache.Parse.Decimal.Token()
                    from a in Sprache.Parse.Decimal.Token()
                    from last in Sprache.Parse.Char(')')
                    select new Color(
                        float.Parse(new string(r.ToArray())),
                        float.Parse(new string(g.ToArray())),
                        float.Parse(new string(b.ToArray())),
                        float.Parse(new string(a.ToArray()))
                    );
            }
        }
    }
}