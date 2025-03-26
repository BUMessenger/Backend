namespace BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

public class UserNotFoundServiceException : Exception
{
    public UserNotFoundServiceException(string message) : base(message) { }
    
    public UserNotFoundServiceException(string message, Exception innerException) : base(message, innerException) { }
}