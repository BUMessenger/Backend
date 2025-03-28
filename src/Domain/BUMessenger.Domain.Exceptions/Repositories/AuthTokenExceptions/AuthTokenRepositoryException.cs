namespace BUMeesenger.Domain.Exceptions.Repositories.AuthTokenExceptions;

public class AuthTokenRepositoryException : Exception
{
    public AuthTokenRepositoryException(string message) : base(message) { }
    
    public AuthTokenRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}