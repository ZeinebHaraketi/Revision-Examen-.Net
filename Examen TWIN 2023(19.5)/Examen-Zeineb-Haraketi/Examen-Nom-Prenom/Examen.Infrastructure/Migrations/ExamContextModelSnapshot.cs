﻿// <auto-generated />
using System;
using Examen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Examen.Infrastructure.Migrations
{
    [DbContext(typeof(ExamContext))]
    partial class ExamContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Tel")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Exemple", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Exemples");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Prestataire", b =>
                {
                    b.Property<int>("PrestataireId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrestataireId"));

                    b.Property<int>("Note")
                        .HasColumnType("int");

                    b.Property<string>("PageInstagram")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PrestataireNom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PrestataireTel")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Zone")
                        .HasColumnType("int");

                    b.HasKey("PrestataireId");

                    b.ToTable("Prestataires");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Prestation", b =>
                {
                    b.Property<int>("PrestationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PrestationId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Intitule")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("PrestataireFk")
                        .HasColumnType("int");

                    b.Property<int>("PrestationType")
                        .HasColumnType("int");

                    b.Property<double>("Prix")
                        .HasColumnType("float");

                    b.HasKey("PrestationId");

                    b.HasIndex("PrestataireFk");

                    b.ToTable("Prestations");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.RDV", b =>
                {
                    b.Property<int>("PrestationFK")
                        .HasColumnType("int");

                    b.Property<int>("ClientFK")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateRDV")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Confirmation")
                        .HasColumnType("bit");

                    b.HasKey("PrestationFK", "ClientFK", "DateRDV");

                    b.HasIndex("ClientFK");

                    b.ToTable("RDVs");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Prestation", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Prestataire", "Prestataire")
                        .WithMany("Prestations")
                        .HasForeignKey("PrestataireFk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prestataire");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.RDV", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Client", "Client")
                        .WithMany("RDVs")
                        .HasForeignKey("ClientFK")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Examen.ApplicationCore.Domain.Prestation", "Prestation")
                        .WithMany("RDVs")
                        .HasForeignKey("PrestationFK")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Prestation");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Client", b =>
                {
                    b.Navigation("RDVs");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Prestataire", b =>
                {
                    b.Navigation("Prestations");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Prestation", b =>
                {
                    b.Navigation("RDVs");
                });
#pragma warning restore 612, 618
        }
    }
}
