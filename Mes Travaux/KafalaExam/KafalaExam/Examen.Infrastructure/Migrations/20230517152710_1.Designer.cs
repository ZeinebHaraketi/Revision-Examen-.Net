﻿// <auto-generated />
using System;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    [DbContext(typeof(ExamContext))]
    [Migration("20230517152710_1")]
    partial class _1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Beneficiary", b =>
                {
                    b.Property<int>("CIN")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CIN"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CIN");

                    b.ToTable("Beneficiaries");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Donation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DonatorFk")
                        .HasColumnType("int");

                    b.Property<int>("DonatorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DonatorId");

                    b.ToTable("Donations");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Donator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Donators");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Exemple", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Exemple");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Kafala", b =>
                {
                    b.Property<int>("BeneficiaryFk")
                        .HasColumnType("int");

                    b.Property<int>("DonatorFk")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("BeneficiaryFk", "DonatorFk", "StartDate", "EndDate");

                    b.HasIndex("DonatorFk");

                    b.ToTable("Kafalas");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Beneficiary", b =>
                {
                    b.OwnsOne("Examen.ApplicationCore.Domain.Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("BeneficiaryCIN")
                                .HasColumnType("int");

                            b1.Property<string>("Adress")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("BeneficiaryCIN");

                            b1.ToTable("Beneficiaries");

                            b1.WithOwner()
                                .HasForeignKey("BeneficiaryCIN");
                        });

                    b.Navigation("Contact")
                        .IsRequired();
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Donation", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Donator", "Donator")
                        .WithMany("Donations")
                        .HasForeignKey("DonatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Donator");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Donator", b =>
                {
                    b.OwnsOne("Examen.ApplicationCore.Domain.Contact", "Contact", b1 =>
                        {
                            b1.Property<int>("DonatorId")
                                .HasColumnType("int");

                            b1.Property<string>("Adress")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Email")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Phone")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("DonatorId");

                            b1.ToTable("Donators");

                            b1.WithOwner()
                                .HasForeignKey("DonatorId");
                        });

                    b.Navigation("Contact")
                        .IsRequired();
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Kafala", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Beneficiary", "Beneficiary")
                        .WithMany()
                        .HasForeignKey("BeneficiaryFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examen.ApplicationCore.Domain.Donator", "Donator")
                        .WithMany()
                        .HasForeignKey("DonatorFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beneficiary");

                    b.Navigation("Donator");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Donator", b =>
                {
                    b.Navigation("Donations");
                });
#pragma warning restore 612, 618
        }
    }
}
