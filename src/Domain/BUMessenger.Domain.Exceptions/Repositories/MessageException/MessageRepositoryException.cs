namespace BUMeesenger.Domain.Exceptions.Repositories.MessageException;

public class MessageRepositoryException : Exception
{
    public MessageRepositoryException(string message) : base(message) { }
    
    public MessageRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}