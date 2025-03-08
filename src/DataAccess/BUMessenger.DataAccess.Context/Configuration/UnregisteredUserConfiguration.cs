using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUMessenger.DataAccess.Context.Configuration;

public class UnregisteredUserConfiguration : IEntityTypeConfiguration<UnregisteredUserDb>
{
    public void Configure(EntityTypeBuilder<UnregisteredUserDb> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        
        builder.Property(u => u.Email).IsRequired();
        
        builder.Property(u => u.ApproveCode).IsRequired();
        
        builder.Property(u => u.ExpiresAtUtc).IsRequired();
    }
}