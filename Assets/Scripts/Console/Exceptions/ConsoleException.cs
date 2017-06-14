using System;
using System.Runtime.Serialization;

namespace Assets.Scripts.Console.Exceptions
{
    public class ConsoleException : Exception
    {
        public ConsoleException(string message) : base(message)
        {
        }

        public ConsoleException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ConsoleException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
