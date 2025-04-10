namespace BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;

public class UnregisteredUserAlreadyExistsServiceException : Exception
{
    public UnregisteredUserAlreadyExistsServiceException(string message) : base(message) { }
    
    public UnregisteredUserAlreadyExistsServiceException(string message, Exception innerException) : base(message, innerException) { }
}