using System;
using System.Linq;
using Sprache;
using UnityEngine;

namespace Assets.Scripts.Console.Parameters
{
    public class Vector3Parameter : AbstractClassParameter<Vector3>
    {
        public Vector3Parameter(string name, bool optional = false) : base(name, optional)
        {
        }

        public override Type GetParamType()
        {
            return typeof(Vector3);
        }

        public override string GetSyntax()
        {
            return string.Format("[x y z]:{0}", Name);
        }

        protected override Parser<Vector3> Parser
        {
            get
            {
                return from first in Sprache.Parse.Char('[')
                    from x in Sprache.Parse.Decimal.Token()
                    from y in Sprache.Parse.Decimal.Token()
                    from z in Sprache.Parse.Decimal.Token()
                    from last in Sprache.Parse.Char(']')
                    select new Vector3(
                        float.Parse(new string(x.ToArray())),
                        float.Parse(new string(y.ToArray())),
                        float.Parse(new string(z.ToArray())));
            }
        }
    }
}
