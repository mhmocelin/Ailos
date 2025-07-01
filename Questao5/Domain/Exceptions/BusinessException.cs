namespace Questao5.Domain.Exceptions;

public class BusinessException : Exception
{
    public string ErrorType { get; }

    public BusinessException(string message, string errorType)
        : base(message)
    {
        ErrorType = errorType;
    }
}
