namespace BUMeesenger.Domain.Exceptions.Services.ChatServiceException;

public class UserNotInChatServiceException : Exception
{
    public UserNotInChatServiceException(string message) : base(message) { }
    
    public UserNotInChatServiceException(string message, Exception innerException) : base(message, innerException) { }
}