namespace BUMeesenger.Domain.Exceptions.Repositories.UserExceptions;

public class UserRepositoryException : Exception
{
    public UserRepositoryException(string message) : base(message) { }
    
    public UserRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}