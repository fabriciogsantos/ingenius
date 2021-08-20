using Ingenius.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;

namespace Ingenius.Data.Context
{
    public class IngeniusContext : DbContext
    {

        public IngeniusContext()
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<SizeProduct> SizeProducts { get; set; }
        public DbSet<Inventory> Stock { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IngeniusContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            Configuracao connectString;
            using (StreamReader config = new StreamReader("configuration.json"))
            {
                string json = config.ReadToEnd();
                connectString = JsonConvert.DeserializeObject<Configuracao>(json);
            }

            optionsBuilder.UseSqlite(connectString.ConnectString);
        }
    }

    public class Configuracao
    {
        public string ConnectString { get; set; }
    }



}
