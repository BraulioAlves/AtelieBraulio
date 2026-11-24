using Microsoft.EntityFrameworkCore;


namespace AtelieBraulio.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Quadro> Quadros => Set<Quadro>();
}