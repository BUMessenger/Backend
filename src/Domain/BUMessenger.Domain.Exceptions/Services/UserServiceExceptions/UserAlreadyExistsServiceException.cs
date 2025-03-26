namespace BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

public class UserAlreadyExistsServiceException : Exception
{
    public UserAlreadyExistsServiceException(string message) : base(message) { }
    
    public UserAlreadyExistsServiceException(string message, Exception innerException) : base(message, innerException) { }
}