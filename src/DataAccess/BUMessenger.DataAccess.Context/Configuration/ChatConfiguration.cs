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
        
        builder
            .HasMany(c => c.Users)
            .WithMany(u => u.Chats)
            .UsingEntity<ChatUserInfoDb>(
                join => join
                    .HasOne(cui => cui.User)
                    .WithMany(u => u.ChatUserInfos)
                    .HasForeignKey(cui => cui.UserId)
                    .OnDelete(DeleteBehavior.Cascade),
                join => join
                    .HasOne(cui => cui.Chat)
                    .WithMany(c => c.ChatUserInfos)
                    .HasForeignKey(cui => cui.ChatId)
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey(cui => cui.Id);
                    join.ToTable("ChatUserInfos");
                });
    }
}