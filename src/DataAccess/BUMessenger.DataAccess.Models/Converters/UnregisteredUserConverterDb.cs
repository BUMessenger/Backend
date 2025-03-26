using System.Diagnostics.CodeAnalysis;
using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;

namespace BUMessenger.DataAccess.Models.Converters;

public static class UnregisteredUserConverterDb
{
    [return: NotNullIfNotNull(nameof(unregisteredUserCreate))]
    public static UnregisteredUserDb? ToDb(this UnregisteredUserCreateWithAdditionalData? unregisteredUserCreate)
    {
        if (unregisteredUserCreate is null)
            return null;

        return new UnregisteredUserDb(
            id: Guid.NewGuid(),
            email: unregisteredUserCreate.Email,
            passwordHashed: unregisteredUserCreate.PasswordHashed,
            approveCode: unregisteredUserCreate.ApproveCode,
            expiresAtUtc: unregisteredUserCreate.ExpiresAtUtc);
    }

    [return: NotNullIfNotNull(nameof(unregisteredUserDb))]
    public static UnregisteredUser? ToDomain(this UnregisteredUserDb? unregisteredUserDb)
    {
        if (unregisteredUserDb is null)
            return null;

        return new UnregisteredUser
        {
            Id = unregisteredUserDb.Id,
            Email = unregisteredUserDb.Email
        };
    }

    [return: NotNullIfNotNull(nameof(unregisteredUserDb))]
    public static UnregisteredUserForAddUser? ToDomainForAddUser(this UnregisteredUserDb? unregisteredUserDb)
    {
        if (unregisteredUserDb is null)
            return null;

        return new UnregisteredUserForAddUser
        {
            Id = unregisteredUserDb.Id,
            Email = unregisteredUserDb.Email,
            PasswordHashed = unregisteredUserDb.PasswordHashed,
            ApproveCode = unregisteredUserDb.ApproveCode,
            ExpiresAtUtc = unregisteredUserDb.ExpiresAtUtc
        };
    }
}