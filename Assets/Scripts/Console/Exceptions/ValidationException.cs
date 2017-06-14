using System;
using System.Runtime.Serialization;
using Assets.Scripts.Console.Parameters;

namespace Assets.Scripts.Console.Exceptions
{
    public class ValidationException : ConsoleException
    {
        public Parameter Parameter { get; set; }

        public ValidationException(string message, Parameter parameter) : base(message)
        {
            parameter = Parameter;
        }

        public ValidationException(string message, Exception innerException, Parameter parameter) : base(message, innerException)
        {
            parameter = Parameter;
        }

        protected ValidationException(SerializationInfo info, StreamingContext context, Parameter parameter) : base(info, context)
        {
            parameter = Parameter;
        }
    }
}
