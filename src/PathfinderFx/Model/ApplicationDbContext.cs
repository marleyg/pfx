using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PathfinderFx.Model;

public class ApplicationDbContext(DbContextOptions options) : IdentityDbContext<ApplicationUser>(options);