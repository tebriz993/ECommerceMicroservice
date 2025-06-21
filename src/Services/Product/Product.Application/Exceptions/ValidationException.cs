using FluentValidation.Results;

namespace Product.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<string> Errors { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            Errors = failures
                .Select(f => f.ErrorMessage)
                .ToList();
        }
    }
}