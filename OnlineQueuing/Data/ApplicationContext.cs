using Microsoft.EntityFrameworkCore;
using OnlineQueuing.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineQueuing.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Date> Dates { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<ServiceDate> ServiceDates { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().Ignore(o => o.TimeSlots);
            modelBuilder.Entity<ServiceDate>()
               .HasKey(sc => new { sc.DataId, sc.ServiceTypeId });
        }
    }
}

