using System;
using System.Linq;
using Sprache;
using UnityEngine;

namespace Assets.Scripts.Console.Parameters
{
    public class Vectro2Parameter : AbstractClassParameter<Vector2>
    {
        public Vectro2Parameter(string name, bool optional = false) : base(name, optional)
        {
        }

        public override Type GetParamType()
        {
            return typeof(Vector2);
        }

        public override string GetSyntax()
        {
            return string.Format("[x y]:{0}", Name);
        }

        protected override Parser<Vector2> Parser
        {
            get
            {
                return from first in Sprache.Parse.Char('[')
                    from x in Sprache.Parse.Decimal.Token()
                    from y in Sprache.Parse.Decimal.Token()
                    from last in Sprache.Parse.Char(']')
                    select new Vector2(
                        float.Parse(new string(x.ToArray())),
                        float.Parse(new string(y.ToArray())));
            }
        }
    }
}
