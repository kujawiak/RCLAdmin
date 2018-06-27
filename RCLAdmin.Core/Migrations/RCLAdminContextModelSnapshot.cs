﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RCLAdmin.Core.Data;

namespace RCLAdmin.Core.Migrations
{
    [DbContext(typeof(RCLAdminContext))]
    partial class RCLAdminContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RCLAdmin.Core.Models.Printer", b =>
                {
                    b.Property<int>("PrinterId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("IP")
                        .IsRequired();

                    b.Property<string>("Info");

                    b.Property<string>("Localisation")
                        .IsRequired();

                    b.Property<string>("Person");

                    b.Property<byte[]>("Photo");

                    b.Property<int>("PrinterTypeId");

                    b.Property<string>("SerialNumber");

                    b.Property<bool>("Status");

                    b.HasKey("PrinterId");

                    b.HasIndex("PrinterTypeId");

                    b.ToTable("Printer");
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.PrinterAccessory", b =>
                {
                    b.Property<int>("PrinterAccessoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Availability");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("PartNumber");

                    b.HasKey("PrinterAccessoryId");

                    b.ToTable("PrinterAccessory");
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.PrinterAccessoryType", b =>
                {
                    b.Property<int>("PrinterAccessoryId");

                    b.Property<int>("PrinterTypeId");

                    b.HasKey("PrinterAccessoryId", "PrinterTypeId");

                    b.HasIndex("PrinterTypeId");

                    b.ToTable("PrinterAccessoryType");
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.PrinterEvent", b =>
                {
                    b.Property<int>("PrinterEventId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comment");

                    b.Property<int>("Counter");

                    b.Property<DateTime>("Date");

                    b.Property<int>("EventType");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<int>("PrinterAccessoryId");

                    b.Property<int>("PrinterId");

                    b.HasKey("PrinterEventId");

                    b.HasIndex("PrinterAccessoryId");

                    b.HasIndex("PrinterId");

                    b.ToTable("PrinterEvent");
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.PrinterType", b =>
                {
                    b.Property<int>("PrinterTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PrinterManufacturer");

                    b.Property<string>("Type");

                    b.HasKey("PrinterTypeId");

                    b.ToTable("PrinterType");
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.Printer", b =>
                {
                    b.HasOne("RCLAdmin.Core.Models.PrinterType", "PrinterType")
                        .WithMany("Printers")
                        .HasForeignKey("PrinterTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.PrinterAccessoryType", b =>
                {
                    b.HasOne("RCLAdmin.Core.Models.PrinterAccessory", "PrinterAccessory")
                        .WithMany("PrinterTypes")
                        .HasForeignKey("PrinterAccessoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RCLAdmin.Core.Models.PrinterType", "PrinterType")
                        .WithMany("Accessories")
                        .HasForeignKey("PrinterTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("RCLAdmin.Core.Models.PrinterEvent", b =>
                {
                    b.HasOne("RCLAdmin.Core.Models.PrinterAccessory", "PrinterAccessory")
                        .WithMany()
                        .HasForeignKey("PrinterAccessoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("RCLAdmin.Core.Models.Printer", "Printer")
                        .WithMany("Events")
                        .HasForeignKey("PrinterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
