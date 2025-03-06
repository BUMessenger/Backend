using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BUMessenger.DataAccess.Context.Configuration;

public class MessageConfiguration : IEntityTypeConfiguration<MessageDb>
{
    public void Configure(EntityTypeBuilder<MessageDb> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedNever();
        
        builder.Property(m => m.SentAtUtc).IsRequired();
        
        builder.Property(m => m.MessageText).IsRequired().HasMaxLength(2000);
        
        builder.HasOne(a => a.Chat)
            .WithMany(c => c.Messages)
            .HasForeignKey(a => a.ChatId);
        
        builder.HasOne(m => m.Creator)
            .WithMany(u => u.Messages)
            .HasForeignKey(a => a.CreatorId);
        
        builder.HasOne(m => m.ParentMessage)
            .WithMany(m => m.ChildMessages)
            .HasForeignKey(m => m.ParentMessageId);
    }
}