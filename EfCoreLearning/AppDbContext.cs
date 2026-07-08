using Microsoft.EntityFrameworkCore;

namespace EfCoreLearning;

public class AppDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=efcorelearning;Username=postgres;Password=4505");
}
}
