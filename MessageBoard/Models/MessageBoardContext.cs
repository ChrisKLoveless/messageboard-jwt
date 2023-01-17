using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Models
{
  public class MessageBoardContext : DbContext
  {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Thread> Threads { get; set; }
    public DbSet<User> Users { get; set; }

    public MessageBoardContext(DbContextOptions<MessageBoardContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<User>()
      .HasData(
        new User { UserId = 1, Name = "Chris"},
        new User { UserId = 2, Name = "Yoonis"},
        new User { UserId = 3, Name = "Robert"}
      );
    }
  }
}
