using System.Diagnostics.CodeAnalysis;
using BUMessenger.Domain.Models.Models.UnregisteredUsers;

namespace BUMessenger.Domain.Models.Models.Converters;

public static class UnregisteredUserConverter
{
    [return: NotNullIfNotNull(nameof(unregisteredUserCreate))]
    public static UnregisteredUserCreateWithAdditionalData? ToDomainWithAdditionalData(
        this UnregisteredUserCreate? unregisteredUserCreate,
        string passwordHashed,
        string approveCode)
    {
        if (unregisteredUserCreate is null)
            return null;

        return new UnregisteredUserCreateWithAdditionalData
        {
            Email = unregisteredUserCreate.Email,
            PasswordHashed = passwordHashed,
            ApproveCode = approveCode,
            ExpiresAtUtc = DateTime.UtcNow.AddHours(1)
        };
    }
}