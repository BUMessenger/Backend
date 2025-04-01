namespace BUMeesenger.Domain.Exceptions.Repositories.UserExceptions;

public class UserNotFoundRepositoryException : Exception
{
    public UserNotFoundRepositoryException(string message) : base(message) { }
    
    public UserNotFoundRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}