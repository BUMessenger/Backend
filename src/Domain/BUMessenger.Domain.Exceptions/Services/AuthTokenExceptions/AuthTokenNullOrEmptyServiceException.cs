namespace BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

public class AuthTokenNullOrEmptyServiceException : Exception
{
    public AuthTokenNullOrEmptyServiceException(string message) : base(message) { }
    
    public AuthTokenNullOrEmptyServiceException(string message, Exception innerException) : base(message, innerException) { }
}