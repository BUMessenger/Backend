namespace BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

public class AuthTokenServiceException : Exception
{
    public AuthTokenServiceException(string message) : base(message) { }
    
    public AuthTokenServiceException(string message, Exception innerException) : base(message, innerException) { }
}