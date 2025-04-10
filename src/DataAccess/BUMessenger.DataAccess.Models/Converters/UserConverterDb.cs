using System.Diagnostics.CodeAnalysis;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.DataAccess.Models.Converters;

public static class UserConverterDb
{
    [return: NotNullIfNotNull(nameof(userCreate))]
    public static UserDb? ToDb(this UserCreateWithPassword? userCreate)
    {
        if (userCreate is null)
            return null;

        return new UserDb(id: Guid.NewGuid(),
            name: "Pidor",
            surname: "Pidorov",
            fatherName: null,
            email: userCreate.Email,
            passwordHashed: userCreate.PasswordHashed);
    }

    [return: NotNullIfNotNull(nameof(userDb))]
    public static User? ToDomain(this UserDb? userDb)
    {
        if (userDb is null)
            return null;

        return new User
        {
            Id = userDb.Id,
            Name = userDb.Name,
            Surname = userDb.Surname,
            Email = userDb.Email,
            Fathername = userDb.FatherName
        };
    }
}