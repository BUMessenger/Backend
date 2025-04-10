using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUMessenger.DataAccess.Context.Configuration;

public class ChatConfiguration : IEntityTypeConfiguration<ChatDb>
{
    public void Configure(EntityTypeBuilder<ChatDb> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();
        
        builder.Property(x => x.ChatName).IsRequired();
        
        builder.HasMany(c => c.Messages)
            .WithOne(m => m.Chat)
            .HasForeignKey(m => m.ChatId);
        
        builder.HasMany(c => c.ChatUserInfos)
            .WithOne(cu => cu.Chat)
            .HasForeignKey(cu => cu.ChatId);
    }
}