using BUMessenger.DataAccess.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BUMessenger.DataAccess.Context;

public class BUMessengerContext : DbContext
{
    public virtual DbSet<AuthTokenDb> AuthTokens { get; set; }
    
    public virtual DbSet<ChatDb> Chats { get; set; }
    
    public virtual DbSet<MessageDb> Messages { get; set; }
    
    public virtual DbSet<UserDb> Users { get; set; }
    
    public virtual DbSet<ChatUserInfoDb> ChatUserInfos { get; set; }
    
    public virtual DbSet<UnregisteredUserDb> UnregisteredUsers { get; set; }
    
    public BUMessengerContext(DbContextOptions<BUMessengerContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BUMessengerContext).Assembly);
    }
}