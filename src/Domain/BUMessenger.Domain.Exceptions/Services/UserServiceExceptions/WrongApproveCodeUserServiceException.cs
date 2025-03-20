namespace BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

public class WrongApproveCodeUserServiceException : Exception
{
    public WrongApproveCodeUserServiceException(string message) : base(message) { }
    
    public WrongApproveCodeUserServiceException(string message, Exception innerException) : base(message, innerException) { }
}