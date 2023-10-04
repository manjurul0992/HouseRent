﻿// <auto-generated />
using System;
using HouseRent.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HouseRent.Server.Migrations
{
    [DbContext(typeof(FlatDbContext))]
    [Migration("20230830132659_ScriptC")]
    partial class ScriptC
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HouseRent.Shared.Models.Flat", b =>
                {
                    b.Property<int>("FlatID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlatID"), 1L, 1);

                    b.Property<string>("FlatName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("money");

                    b.HasKey("FlatID");

                    b.ToTable("Flats");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.Rent", b =>
                {
                    b.Property<int>("RentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RentID"), 1L, 1);

                    b.Property<DateTime?>("BookedDate")
                        .HasColumnType("date");

                    b.Property<DateTime>("RentDate")
                        .HasColumnType("date");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TenantID")
                        .HasColumnType("int");

                    b.HasKey("RentID");

                    b.HasIndex("TenantID");

                    b.ToTable("Rents");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.RentItem", b =>
                {
                    b.Property<int>("RentID")
                        .HasColumnType("int");

                    b.Property<int>("FlatID")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("RentID", "FlatID");

                    b.HasIndex("FlatID");

                    b.ToTable("RentItems");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.Tenant", b =>
                {
                    b.Property<int>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantID"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TenantName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TenantID");

                    b.ToTable("Tenants");

                    b.HasData(
                        new
                        {
                            TenantID = 1,
                            Address = "Mirpur",
                            Email = "Manjurul12@gmail.com",
                            TenantName = "Manjurul"
                        },
                        new
                        {
                            TenantID = 2,
                            Address = "Mirpur",
                            Email = "Manjurul12@gmail.com",
                            TenantName = "syed"
                        },
                        new
                        {
                            TenantID = 3,
                            Address = "Mirpur",
                            Email = "Manjurul12@gmail.com",
                            TenantName = "syed"
                        });
                });

            modelBuilder.Entity("HouseRent.Shared.Models.Rent", b =>
                {
                    b.HasOne("HouseRent.Shared.Models.Tenant", "Tenant")
                        .WithMany("Rents")
                        .HasForeignKey("TenantID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.RentItem", b =>
                {
                    b.HasOne("HouseRent.Shared.Models.Flat", "Flat")
                        .WithMany("RentItems")
                        .HasForeignKey("FlatID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HouseRent.Shared.Models.Rent", "Rent")
                        .WithMany("RentItems")
                        .HasForeignKey("RentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Flat");

                    b.Navigation("Rent");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.Flat", b =>
                {
                    b.Navigation("RentItems");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.Rent", b =>
                {
                    b.Navigation("RentItems");
                });

            modelBuilder.Entity("HouseRent.Shared.Models.Tenant", b =>
                {
                    b.Navigation("Rents");
                });
#pragma warning restore 612, 618
        }
    }
}
