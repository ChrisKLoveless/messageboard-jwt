using Microsoft.EntityFrameworkCore;

namespace MessageBoard.Models
{
  public class MessageBoardContext : DbContext
  {
    public DbSet<Post> Posts { get; set; }
    public DbSet<Threads> Threads { get; set; }
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
      builder.Entity<Threads>()
      .HasData(
        new Threads { UserId = 1, ThreadsId = 1, Title = "News"},
        new Threads { UserId = 2, ThreadsId = 2, Title = "Memes"},
        new Threads { UserId = 3, ThreadsId = 3, Title = "Sports"}
      );
      builder.Entity<Post>()
      .HasData(
        new Post { UserId = 1, PostId = 1, ThreadsId = 1, Body = "News"},
        new Post { UserId = 2, PostId = 2, ThreadsId = 2, Body = "Memes"},
        new Post { UserId = 3, PostId = 3, ThreadsId = 3, Body = "Sports"}
      );
    }
  }
}
