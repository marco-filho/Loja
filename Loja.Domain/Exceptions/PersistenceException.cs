using System.Text.Json;

namespace Loja.Domain.Exceptions
{
    public class PersistenceException : Exception
    {
        public const string ExceptionType = "PersistenceException";
        public const string _message = "There was an error saving data.";

        public PersistenceException() : base(_message)
        {
        }

        public PersistenceException(object entitty) : base($"{_message} Entity: {JsonSerializer.Serialize(entitty)}")
        {
        }

        public PersistenceException(string message) : base(message)
        {
        }

        public PersistenceException(Exception exception) : base(exception.GetType().Name, exception)
        {
        }

        public PersistenceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}