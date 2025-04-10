namespace BUMeesenger.Domain.Exceptions.Services.UnregisteredUserExceptions;

public class UnregisteredUserServiceException : Exception
{
    public UnregisteredUserServiceException(string message) : base(message) { }
    
    public UnregisteredUserServiceException(string message, Exception innerException) : base(message, innerException) { }
}