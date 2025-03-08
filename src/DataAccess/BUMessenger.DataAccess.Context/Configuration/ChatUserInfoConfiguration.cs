using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUMessenger.DataAccess.Context.Configuration;

public class ChatUserInfoConfiguration : IEntityTypeConfiguration<ChatUserInfoDb>
{
    public void Configure(EntityTypeBuilder<ChatUserInfoDb> builder)
    {
        builder.HasKey(cu => cu.Id);
        builder.Property(cu => cu.ChatId).ValueGeneratedNever();

        builder.HasOne(cu => cu.Chat)
            .WithMany(c => c.ChatUserInfos)
            .HasForeignKey(cu => cu.ChatId);
        
        builder.HasOne(cu => cu.User)
            .WithMany(u => u.ChatUserInfos)
            .HasForeignKey(cu => cu.UserId);
        
        builder.HasOne(cu => cu.LastReadMessage)
            .WithMany(m => m.ChatUserInfos)
            .HasForeignKey(cu => cu.LastReadMessageId);
    }
}