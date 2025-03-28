namespace BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

public class AuthTokenNotFoundServiceException : Exception
{
    public AuthTokenNotFoundServiceException(string message) : base(message) { }
    
    public AuthTokenNotFoundServiceException(string message, Exception innerException) : base(message, innerException) { }
}