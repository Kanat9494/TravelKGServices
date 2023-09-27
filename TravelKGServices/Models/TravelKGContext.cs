namespace TravelKGServices.Models;

public class TravelKGContext : DbContext
{
    public TravelKGContext(DbContextOptions<TravelKGContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
}
