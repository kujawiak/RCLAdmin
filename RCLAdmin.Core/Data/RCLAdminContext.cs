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
                //.HasRequired<PrinterType>(a => a.PrinterType)
                //.WithMany(a => a.Printers)
                //.WillCascadeOnDelete(false)
                ;

            modelBuilder.Entity<PrinterType>()
                .HasMany<Printer>(a => a.Printers);

            modelBuilder.Entity<Printer>()
                .HasMany<PrinterEvent>(a => a.Events);

            modelBuilder.Entity<PrinterType>()
                .HasMany<PrinterAccessory>(a => a.Accessories)
                //.WithMany(a => a.PrinterTypes)
                //.Map(cs =>
                //{
                //    cs.ToTable("PrinterAccesoryLinks");
                //    cs.MapLeftKey("TypeRefId");
                //    cs.MapRightKey("AccesoryRefId");
                //})
                ;
        }
    }
}
