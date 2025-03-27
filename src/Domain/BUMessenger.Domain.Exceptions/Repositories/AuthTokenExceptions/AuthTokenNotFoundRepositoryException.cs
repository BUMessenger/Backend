using BUMeesenger.Domain.Exceptions.Services.AuthTokenExceptions;

namespace BUMeesenger.Domain.Exceptions.Repositories.AuthTokenExceptions;

public class AuthTokenNotFoundRepositoryException : Exception
{
    public AuthTokenNotFoundRepositoryException(string message) : base(message) { }
    
    public AuthTokenNotFoundRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}