using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using IslaiduValdymoSistema.Models;
using Microsoft.EntityFrameworkCore;

namespace IslaiduValdymoSistema.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
: base(options)
{
}
    public DbSet<Kategorija> Kategorijos { get; set; } = default!;
    public DbSet<Islaida> Islaidos { get; set; } = default!;
}
}
