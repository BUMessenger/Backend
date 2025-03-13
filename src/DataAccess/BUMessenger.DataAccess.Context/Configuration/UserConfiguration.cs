using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUMessenger.DataAccess.Context.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<UserDb>
{
    public void Configure(EntityTypeBuilder<UserDb> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        
        builder.Property(u => u.Name).IsRequired();
        
        builder.Property(u => u.Surname).IsRequired();
        
        builder.Property(u => u.FatherName);
        
        builder.Property(u => u.Email).IsRequired();
        
        builder.Property(u => u.PasswordHashed).IsRequired();

        builder.HasOne(u => u.AuthToken)
            .WithOne(a => a.User)
            .HasForeignKey<AuthTokenDb>(a => a.UserId);
        
        builder.HasMany(u => u.Messages)
            .WithOne(m => m.Creator)
            .HasForeignKey(m => m.CreatorId);
        
        builder.HasMany(u => u.ChatUserInfos)
            .WithOne(cu => cu.User)
            .HasForeignKey(cu => cu.UserId);
    }
}