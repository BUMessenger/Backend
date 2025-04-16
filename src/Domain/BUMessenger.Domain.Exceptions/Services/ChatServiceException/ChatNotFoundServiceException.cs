namespace BUMeesenger.Domain.Exceptions.Services.ChatServiceException;

public class ChatNotFoundServiceException : Exception
{
    public ChatNotFoundServiceException(string message) : base(message) { }
    
    public ChatNotFoundServiceException(string message, Exception innerException) : base(message, innerException) { }
}