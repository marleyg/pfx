using Microsoft.EntityFrameworkCore;

namespace PathfinderFx.Model;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder) { }
}