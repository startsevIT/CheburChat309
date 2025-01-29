using Domain.BusinessEntites.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataBase;

public class SqLiteDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public SqLiteDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=CheburChatDataBase.db");
    }
}
