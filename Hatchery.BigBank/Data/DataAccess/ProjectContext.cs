using Hatchery.BigBank.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hatchery.BigBank.Data.DataAccess
{
    public class ProjectContext : DbContext
    {
        private readonly IConfiguration _config;

        public ProjectContext(DbContextOptions options, IConfiguration config) : base(options) {
            _config = config;
        }

        public DbSet<Partner> Partners { get; set; }
        public DbSet<LoanRequest> Calculators { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("BankingAPI"));
            
        }
    }
}
