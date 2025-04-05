namespace BUMeesenger.Domain.Exceptions.Repositories.ChatExceptions;

public class ChatNotFoundRepositoryException : Exception
{
    public ChatNotFoundRepositoryException(string message) : base(message) { }
    
    public ChatNotFoundRepositoryException(string message, Exception innerException) : base(message, innerException) { }
}