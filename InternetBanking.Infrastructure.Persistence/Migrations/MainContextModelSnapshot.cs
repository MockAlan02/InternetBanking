﻿// <auto-generated />
using System;
using InternetBanking.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBanking.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(MainContext))]
    partial class MainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBanking.Domain.Entities.BasicProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("IdentifierAccount")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Products", (string)null);

                    b.HasDiscriminator<int>("ProductType").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.BasicUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdentificationCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasDiscriminator<int>("UserType").HasValue(0);

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTransanction")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.CreditCard", b =>
                {
                    b.HasBaseType("InternetBanking.Domain.Entities.BasicProduct");

                    b.Property<decimal>("Limit")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.Property<decimal>("LoanAmount")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasDiscriminator().HasValue(3);
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.Loan", b =>
                {
                    b.HasBaseType("InternetBanking.Domain.Entities.BasicProduct");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.SavingsAccount", b =>
                {
                    b.HasBaseType("InternetBanking.Domain.Entities.BasicProduct");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.Admin", b =>
                {
                    b.HasBaseType("InternetBanking.Domain.Entities.BasicUser");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.Customer", b =>
                {
                    b.HasBaseType("InternetBanking.Domain.Entities.BasicUser");

                    b.Property<decimal>("Amount")
                        .HasPrecision(18, 6)
                        .HasColumnType("decimal(18,6)");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.BasicProduct", b =>
                {
                    b.HasOne("InternetBanking.Domain.Entities.Customer", null)
                        .WithMany("Products")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("InternetBanking.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
