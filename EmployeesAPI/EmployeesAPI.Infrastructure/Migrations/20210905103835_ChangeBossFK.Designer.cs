﻿// <auto-generated />
using System;
using EmployeesAPI.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EmployeesAPI.Infrastructure.Migrations
{
    [DbContext(typeof(EmployeesDbContext))]
    [Migration("20210905103835_ChangeBossFK")]
    partial class ChangeBossFK
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("EmployeesAPI.Domain.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid?>("BossId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("EmploymentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("HomeAddress")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BossId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EmployeesAPI.Domain.Models.Employee", b =>
                {
                    b.HasOne("EmployeesAPI.Domain.Models.Employee", "Boss")
                        .WithMany()
                        .HasForeignKey("BossId");

                    b.Navigation("Boss");
                });
#pragma warning restore 612, 618
        }
    }
}
