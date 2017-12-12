using System;
using IBM.FCAGroup.FiatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IBM.FCAGroup.FiatApp.DataAccess
{
    public partial class MainContext : DbContext
    {
        public DbSet<Item> Justification { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ItemMap(modelBuilder);
        }
    }
}
