namespace Seals.Duv.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; }

        public ValidationException(string message) : base(message)
        {
            Errors = [message];
        }

        public ValidationException(IEnumerable<string> errors) : base("Validation failed")
        {
            Errors = errors;
        }
    }
}
