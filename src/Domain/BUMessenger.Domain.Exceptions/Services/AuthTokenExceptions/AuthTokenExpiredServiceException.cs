namespace BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

public class AuthTokenExpiredServiceException : Exception
{
    public AuthTokenExpiredServiceException(string message) : base(message) { }
    
    public AuthTokenExpiredServiceException(string message, Exception innerException) : base(message, innerException) { }
}