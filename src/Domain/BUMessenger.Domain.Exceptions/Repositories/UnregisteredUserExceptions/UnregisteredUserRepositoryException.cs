namespace BUMeesenger.Domain.Exceptions.Repositories.UnregisteredUserExceptions;

public class UnregisteredUserRepositoryException : Exception
{
    public UnregisteredUserRepositoryException(string message) : base(message) { }
    
    public UnregisteredUserRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}