using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models.Users;
using BUMessenger.Web.Dto.Models.Users;

namespace BUMessenger.Web.Dto.Converters;

public static class UserConverterDto
{
    [return: NotNullIfNotNull(nameof(userCreateDto))]
    public static UserCreate? ToDomain(this UserCreateDto? userCreateDto)
    {
        if (userCreateDto is null)
            return null;

        return new UserCreate
        {
            Email = userCreateDto.Email,
            ApproveCode = userCreateDto.ApproveCode
        };
    }
}