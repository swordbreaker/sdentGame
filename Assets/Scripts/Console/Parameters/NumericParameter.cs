using System;
using Assets.Scripts.Console.Exceptions;

namespace Assets.Scripts.Console.Parameters
{
    public class NumericParameter<T> : Parameter where T: struct,
                                              IComparable,
                                              IComparable<T>,
                                              IConvertible,
                                              IEquatable<T>,
                                              IFormattable
    {
        public Range<T>? Range { get; private set; }

        public static readonly Predicate<string> CanParsePredicate = s =>
        {
            float tmp;
            return float.TryParse(s, out tmp);
        };

        public NumericParameter(string name, Func<string, T> parser, bool optional) : base(name, parser, CanParsePredicate, optional: optional)
        {
            ValidationPredicate = s =>
            {
                if (Range.HasValue)
                {
                    if (CanParse(s))
                    {
                        T f = (T)Parse(s);
                        return Range.Value.IsInRange(f);
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            };
        }

        public override void Validate(string value)
        {
            if (!IsValid(value))
            {
                throw new ValidationException(string.Format("Number is out of Range {0}", (Range.HasValue) ? Range.Value.ToString() : ""), this);
            }
        }

        public void SetRange(T from, T till)
        {
            Range = new Range<T>(from, till, (value, range) => (value.CompareTo(range.Form) >= 0 && value.CompareTo(range.Till) < 0));
        }

        public override Type GetParamType()
        {
            return typeof(T);
        }
    }
}
