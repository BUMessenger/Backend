using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.Domain.Models.Models.Converters;

public static class UserConverter
{
    [return: NotNullIfNotNull(nameof(userCreate))]
    public static UserCreateWithPassword? ToUserCreateWithPassword(this UserCreate? userCreate,
        string passwordHashed)
    {
        if (userCreate is null)
            return null;

        return new UserCreateWithPassword
        {
            Email = userCreate.Email,
            PasswordHashed = passwordHashed,
        };
    }
}