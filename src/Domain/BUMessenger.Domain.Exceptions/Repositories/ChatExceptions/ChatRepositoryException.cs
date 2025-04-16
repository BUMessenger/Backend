namespace BUMeesenger.Domain.Exceptions.Repositories.ChatExceptions;

public class ChatRepositoryException : Exception
{
    public ChatRepositoryException(string message) : base(message) { }
    
    public ChatRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}