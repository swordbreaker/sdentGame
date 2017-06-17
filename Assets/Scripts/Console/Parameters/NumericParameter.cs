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

        private readonly Func<string, T> _parser;

        public NumericParameter(string name, Func<string, T> parser, bool optional) : base(name, optional)
        {
            _parser = parser;
        }

        protected override object ParseValue(string s)
        {
            return _parser(s);
        }

        public override bool CanParse(string value)
        {
            float tmp;
            return float.TryParse(value, out tmp);
        }

        public override bool IsValid(string value)
        {
            if (Range.HasValue)
            {
                if (CanParse(value))
                {
                    T f = (T)Parse(value, false);
                    return Range.Value.IsInRange(f);
                }
                else
                {
                    return false;
                }
            }
            return true;
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
            Range = new Range<T>(from, till, (value, range) => (value.CompareTo(range.Form) >= 0 && value.CompareTo(range.Till) <= 0));
        }

        public override Type GetParamType()
        {
            return typeof(T);
        }
    }
}
