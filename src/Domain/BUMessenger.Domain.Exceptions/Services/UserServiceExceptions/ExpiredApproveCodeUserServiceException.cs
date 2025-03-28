namespace BUMeesenger.Domain.Exceptions.Services.UserServiceExceptions;

public class ExpiredApproveCodeUserServiceException : Exception
{
    public ExpiredApproveCodeUserServiceException(string message) : base(message) { }
    
    public ExpiredApproveCodeUserServiceException(string message, Exception innerException) : base(message, innerException) { }
}