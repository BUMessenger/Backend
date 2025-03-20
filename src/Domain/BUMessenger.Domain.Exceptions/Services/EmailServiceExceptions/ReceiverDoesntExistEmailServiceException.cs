namespace BUMeesenger.Domain.Exceptions.Services.EmailServiceExceptions;

public class ReceiverDoesntExistEmailServiceException : Exception
{
    public ReceiverDoesntExistEmailServiceException(string message) : base(message) { }
    
    public ReceiverDoesntExistEmailServiceException(string message, Exception innerException) : base(message, innerException) { }
}