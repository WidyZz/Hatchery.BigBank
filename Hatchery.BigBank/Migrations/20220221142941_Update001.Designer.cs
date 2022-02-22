﻿// <auto-generated />
using System;
using Hatchery.BigBank.Data.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Hatchery.BigBank.Migrations
{
    [DbContext(typeof(ProjectContext))]
    [Migration("20220221142941_Update001")]
    partial class Update001
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Hatchery.BigBank.Data.Entities.Calculator", b =>
                {
                    b.Property<int>("CalculatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CalculatorId"), 1L, 1);

                    b.Property<decimal>("CreditValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("DueDate")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PartnerId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CalculatorId");

                    b.HasIndex("PartnerId");

                    b.ToTable("Calculators");
                });

            modelBuilder.Entity("Hatchery.BigBank.Data.Entities.Partner", b =>
                {
                    b.Property<int>("PartnerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PartnerId"), 1L, 1);

                    b.Property<string>("PartnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PartneredSince")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("PartneredTo")
                        .HasColumnType("datetime2");

                    b.HasKey("PartnerId");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("Hatchery.BigBank.Data.Entities.Calculator", b =>
                {
                    b.HasOne("Hatchery.BigBank.Data.Entities.Partner", "Partner")
                        .WithMany("Calculators")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Hatchery.BigBank.Data.Entities.Partner", b =>
                {
                    b.Navigation("Calculators");
                });
#pragma warning restore 612, 618
        }
    }
}