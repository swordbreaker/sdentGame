using System;
using Assets.Scripts.Console.Exceptions;

namespace Assets.Scripts.Console.Parameters
{
    public abstract class Parameter : IParameter
    {
        public string Name { get; private set; }
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
            throw new ParameterException(string.Format("Cannot parse {0} to type {1}", value, this.GetParamType().Name), this);
        }

        public bool CanParse(string value)
        {
            return _canParsePredicate(value);
        }

        public bool IsValid(string value)
        {
            return ValidationPredicate(value);
        }

        public abstract void Validate(string value);

        public abstract Type GetParamType();
    }
}
