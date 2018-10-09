using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TransferAppCQRS.Domain.Models;
using TransferAppCQRS.Infra.Data.Mappings;

namespace TransferAppCQRS.Infra.Data.Context
{
    public class TransferAppContext : DbContext
    {
        public DbSet<Transfer> Transfers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TransferMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // define the database to use
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

    }
}
