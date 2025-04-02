namespace BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

public class UserWrongPasswordServiceException : Exception
{
    public UserWrongPasswordServiceException(string message) : base(message) { }
    
    public UserWrongPasswordServiceException(string message, Exception innerException) : base(message, innerException) { }
}