namespace BUMeesenger.Domain.Exceptions.Services.MessageServiceException;

public class MessageServiceException : Exception
{
    public MessageServiceException(string message) : base(message) { }
    
    public MessageServiceException(string message, Exception innerException) : base(message, innerException) { }
}