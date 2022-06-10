using System;

namespace AntesQueVenca.Domain.Exceptions
{
    public class ValidationException: Exception
    {
        public ValidationException(string message):base(message)
        {

        }
    }
}
