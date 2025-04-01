using BUMessenger.DataAccess.Models.Models;
using BUMessenger.Domain.Models.Models.Users;

namespace BUMessenger.DataAccess.Repositories.Extensions;

public static class UserFiltersExtension
{
    public static IQueryable<UserDb> ApplyUserFilters(this IQueryable<UserDb> users, UserFilters userFilters)
    {
        if (userFilters.Name is not null)
            users = users.Where(u => u.Name == userFilters.Name);
        if (userFilters.Surname is not null)
            users = users.Where(u => u.Surname == userFilters.Surname);
        if (userFilters.Fathername is not null)
            users = users.Where(u => u.FatherName == userFilters.Fathername);
        if (userFilters.Email is not null)
            users = users.Where(u => u.Email == userFilters.Email);
        if (userFilters.ChatId is not null)
            users = users.Where(u => u.ChatUserInfos.Any(c => c.ChatId == userFilters.ChatId));
        return users;
    }
}