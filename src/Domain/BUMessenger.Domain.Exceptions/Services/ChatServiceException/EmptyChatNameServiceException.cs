namespace BUMeesenger.Domain.Exceptions.Services.ChatServiceException;

public class EmptyChatNameServiceException : Exception
{
    public EmptyChatNameServiceException(string message) : base(message) { }
    
    public EmptyChatNameServiceException(string message, Exception innerException) : base(message, innerException) { }
}