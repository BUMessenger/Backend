namespace BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;

public class UnregisteredUserNotFoundServiceException : Exception
{
    public UnregisteredUserNotFoundServiceException(string message) : base(message) { }
    
    public UnregisteredUserNotFoundServiceException(string message, Exception innerException) : base(message, innerException) { }
}