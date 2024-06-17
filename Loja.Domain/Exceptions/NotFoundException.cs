using System.Text.Json;

namespace Loja.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public const string ExceptionType = "NotFoundException";
        public const string _message = "Url address not found";

        public NotFoundException() : base(_message)
        {
        }

        public NotFoundException(object parameters) : base($"{_message} Parameters: {JsonSerializer.Serialize(parameters)}")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(Exception exception) : base(exception.GetType().Name, exception)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
