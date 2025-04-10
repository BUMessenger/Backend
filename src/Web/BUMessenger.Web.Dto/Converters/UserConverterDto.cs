using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models;
using BUMessenger.Domain.Models.Models.Users;
using BUMessenger.Web.Dto.Models;
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

    [return: NotNullIfNotNull(nameof(userFiltersDto))]
    public static UserFilters? ToDomain(this UserFiltersDto? userFiltersDto)
    {
        if (userFiltersDto is null)
            return null;

        return new UserFilters
        {
            Name = userFiltersDto.Name,
            Surname = userFiltersDto.Surname,
            Fathername = userFiltersDto.Fathername,
            Email = userFiltersDto.Email,
            ChatId = userFiltersDto.ChatId
        };
    }

    [return: NotNullIfNotNull(nameof(user))]
    public static UserDto? ToDto(this User? user)
    {
        if (user is null)
            return null;

        return new UserDto(id: user.Id,
            name: user.Name,
            surname: user.Surname,
            fathername: user.Fathername,
            email: user.Email);
    }

    [return: NotNullIfNotNull(nameof(userPasswordRecoveryDto))]
    public static UserPasswordRecovery? ToDomain(this UserPasswordRecoveryDto? userPasswordRecoveryDto)
    {
        if (userPasswordRecoveryDto is null)
            return null;

        return new UserPasswordRecovery
        {
            Email = userPasswordRecoveryDto.Email
        };
    }

    [return: NotNullIfNotNull(nameof(userNameUpdateDto))]
    public static UserNameUpdate? ToDomain(this UserNameUpdateDto? userNameUpdateDto)
    {
        if (userNameUpdateDto is null)
            return null;

        return new UserNameUpdate
        {
            Name = userNameUpdateDto.Name,
            Surname = userNameUpdateDto.Surname,
            Fathername = userNameUpdateDto.Fathername
        };
    }

    [return: NotNullIfNotNull(nameof(userPasswordUpdateDto))]
    public static UserPasswordUpdate? ToDomain(this UserPasswordUpdateDto? userPasswordUpdateDto)
    {
        if (userPasswordUpdateDto is null)
            return null;

        return new UserPasswordUpdate
        {
            OldPassword = userPasswordUpdateDto.OldPassword,
            NewPassword = userPasswordUpdateDto.NewPassword
        };
    }

    [return: NotNullIfNotNull(nameof(users))]
    public static PagedDto<UserDto>? ToDto(this Paged<User>? users)
    {
        if (users is null)
            return null;

        return new PagedDto<UserDto>(count: users.Count,
            items: users.Items.ConvertAll(ToDto)!);
    }
}