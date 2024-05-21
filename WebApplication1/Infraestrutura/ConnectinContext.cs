using ChallengeApi.Domain.Models;
using ChallengeApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ChallengeApi.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Noticia> noticia { get; set; }
        public DbSet<User> user { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(
                "Server=localhost;" +
                "Port=5433;Database=challenge;" +
                "User Id=postgres;" +
                "Password=Nojoke@23;");
    }
}
