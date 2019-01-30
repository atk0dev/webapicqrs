using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerApi.Data.Models;
using CustomerApi.Data.Models.Sql;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data
{
    public class CustomerDatabaseContext : DbContext
    {
        public CustomerDatabaseContext(DbContextOptions<CustomerDatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRecord>()
                .HasMany(x => x.Phones);

            modelBuilder.Entity<PhoneRecord>()
                .HasOne(p => p.Customer)
                .WithMany(c => c.Phones)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<CustomerRecord> Customers { get; set; }
    }
}
