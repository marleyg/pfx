using Microsoft.EntityFrameworkCore;

namespace PathfinderFx.Model;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder) { }
}