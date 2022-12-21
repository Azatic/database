using Microsoft.EntityFrameworkCore;

namespace asd;

public class ApplicationContext : DbContext
{
    //  public DbSet<User> Users => Set<User>();
    public DbSet<Movie> MovieDBs => Set<Movie>();
    public DbSet<ActorsDB> ActorsDBs => Set<ActorsDB>();
    public DbSet<TagsDb> TagsDbs => Set<TagsDb>();

    //  public DbSet<top10> Top10s => Set<top10>();

    public ApplicationContext()
    {
         //  Database.EnsureDeleted();
         // Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("server=localhost;Port=5432;database=Semen;userId=postgres;password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Movie>().HasMany(m => m.top10)
            .WithMany();
    }
}