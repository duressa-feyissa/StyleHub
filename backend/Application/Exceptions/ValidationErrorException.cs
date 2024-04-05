using FluentValidation.Results;

namespace Application.Exceptions
{
    public class ValidationErrorException : ApplicationException
    {
        public List<string> Errors { get; set; } = new List<string>();

        public ValidationErrorException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Errors.Add(error.ErrorMessage);
            }
        }
    }
}