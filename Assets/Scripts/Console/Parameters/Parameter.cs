using System;
using Assets.Scripts.Console.Exceptions;

namespace Assets.Scripts.Console.Parameters
{
    public abstract class Parameter : IParameter
    {
        public string  Name { get; private set; }
        public bool Optional { get; private set; }

        private Func<string, object> _parser;
        private Predicate<string> _canParsePredicate;
        protected Predicate<string> ValidationPredicate;

        private static readonly Predicate<string> DefaultValidationPredicate = s => true;

        public Parameter(string name, Func<string, object> parser, Predicate<string> canParsePredicate, Predicate<string> validationPredicate = null, bool optional = false)
        {
            _parser = parser;
            _canParsePredicate = canParsePredicate;
            ValidationPredicate = (validationPredicate == null) ? DefaultValidationPredicate : validationPredicate;
            Name = name;
            Optional = optional;
        }

        public object Parse(string value)
        {
            Validate(value);
            if (CanParse(value))
            {
                return _parser(value);
            }
            else
            {
                throw new FormatException(string.Format("Cannot parse {0} to type {1}", value, this.GetParamType().Name));
            }
        }

        public bool CanParse(string value)
        {
            return _canParsePredicate(value);
        }

        //public virtual void Validate(string value)
        //{
        //    if (!IsValid(value))
        //    {
        //        throw new ValidationException<T>(string.Format("Validation failed for the parameter of Type {0}", typeof(T).Name), this);
        //    }
        //}

        public bool IsValid(string value)
        {
            return ValidationPredicate(value);
        }

        public abstract void Validate(string value);

        public abstract Type GetParamType();

        //public Type GetParamType()
        //{
        //    return typeof(T);
        //}
    }
}
