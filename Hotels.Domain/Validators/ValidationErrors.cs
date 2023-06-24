using FluentValidation.Results;

namespace Hotels.Services.Validators;

public record ValidationErrors(IEnumerable<ValidationFailure> Errors)
{
    public ValidationErrors(ValidationFailure error) : this(new[] { error })
    {
    }
}
