using Mango.Service.EmailAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace Mango.services.EmailAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<EmailLogger> EmailLoggers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          
        }

    }
}
