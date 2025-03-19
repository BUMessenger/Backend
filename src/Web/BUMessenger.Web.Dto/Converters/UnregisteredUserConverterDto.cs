using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;
using BUMessenger.Web.Dto.Models;

namespace BUMessenger.Web.Dto.Converters;

public static class UnregisteredUserConverterDto
{
    [return: NotNullIfNotNull(nameof(unregisteredUserCreateDto))]
    public static UnregisteredUserCreate? ToDomain(this UnregisteredUserCreateDto? unregisteredUserCreateDto)
    {
        if (unregisteredUserCreateDto is null)
            return null;

        return new UnregisteredUserCreate
        {
            Email = unregisteredUserCreateDto.Email,
            Password = unregisteredUserCreateDto.Password
        };
    }

    [return: NotNullIfNotNull(nameof(unregisteredUser))]
    public static UnregisteredUserDto? ToDto(this UnregisteredUser? unregisteredUser)
    {
        if (unregisteredUser is null)
            return null;
        
        return new UnregisteredUserDto(id: unregisteredUser.Id, email: unregisteredUser.Email);
    }
}