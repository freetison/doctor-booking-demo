using Api.Common.Exceptions;

namespace Api.Common.Models.Validators;

public interface IValidationExceptionProcessor<T> where T : class
{
    Task<T> ProcessAsync(CustomException? exceptionContext);
}
