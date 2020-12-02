
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmaBackend.Models;

namespace PharmaBackend.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Medicine> Medicine { get; set; }
        private readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(_configuration.GetConnectionString("WebApiDatabase"));
    }
}