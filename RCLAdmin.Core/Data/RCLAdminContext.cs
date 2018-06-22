using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.Data
{
    public class RCLAdminContext : DbContext
    {
        public RCLAdminContext(DbContextOptions<RCLAdminContext> options) : base(options)
        {
        }

        public DbSet<Printer> Printers { get; set; }
        public DbSet<PrinterType> PrinterTypes { get; set; }
        public DbSet<PrinterEvent> PrinterEvents { get; set; }
        public DbSet<PrinterAccessory> PrinterAccessories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Printer>()
                .ToTable("Printer");
            modelBuilder.Entity<PrinterType>()
                .ToTable("PrinterType")
                .HasMany<Printer>(a => a.Printers);
            modelBuilder.Entity<PrinterEvent>()
                .ToTable("PrinterEvent");
            modelBuilder.Entity<PrinterAccessory>()
                .ToTable("PrinterAccessory")
            modelBuilder.Entity<PrinterAccessoryType>()
                .ToTable("PrinterAccessoryType")
                .HasKey(a => new { a.PrinterAccessoryId, a.PrinterTypeId });
        }
    }
}
