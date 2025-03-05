using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUMessenger.DataAccess.Context.Configuration;

public class AuthTokenConfiguration : IEntityTypeConfiguration<AuthTokenDb>
{
    public void Configure(EntityTypeBuilder<AuthTokenDb> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();
        
        builder.Property(a => a.RefreshToken).IsRequired();
        
        builder.Property(a => a.ExpiresAtUtc).IsRequired();

        builder.HasOne(a => a.User)
            .WithOne(u => u.AuthToken)
            .HasForeignKey<AuthTokenDb>(a => a.UserId);
    }
}