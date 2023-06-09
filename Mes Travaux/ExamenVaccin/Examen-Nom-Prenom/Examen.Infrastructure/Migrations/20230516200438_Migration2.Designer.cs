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
    [Migration("20230516200438_Migration2")]
    partial class Migration2
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

            modelBuilder.Entity("Examen.ApplicationCore.Domain.CentreVaccination", b =>
                {
                    b.Property<int>("CentreVaccinationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CentreVaccinationId"));

                    b.Property<int>("Capacite")
                        .HasColumnType("int");

                    b.Property<int>("NbChaises")
                        .HasColumnType("int");

                    b.Property<int>("NumTelephone")
                        .HasColumnType("int");

                    b.Property<string>("ResponsableCentre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CentreVaccinationId");

                    b.ToTable("CentreVaccinations");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Citoyen", b =>
                {
                    b.Property<string>("CIN")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CitoyenId")
                        .HasColumnType("int");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumeroEvax")
                        .HasColumnType("int");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Telephone")
                        .HasColumnType("int");

                    b.HasKey("CIN");

                    b.ToTable("Citoyens");
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

            modelBuilder.Entity("Examen.ApplicationCore.Domain.RendezVous", b =>
                {
                    b.Property<string>("CitoyenId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("VaccinId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateVaccination")
                        .HasColumnType("datetime2");

                    b.Property<string>("CodeInfermiere")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbrDoses")
                        .HasColumnType("int");

                    b.HasKey("CitoyenId", "VaccinId", "DateVaccination");

                    b.HasIndex("VaccinId");

                    b.ToTable("RendezVous");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Vaccin", b =>
                {
                    b.Property<int>("VaccinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VaccinId"));

                    b.Property<int>("CentreVaccinationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateValidite")
                        .HasColumnType("datetime2");

                    b.Property<string>("Fournisseur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantite")
                        .HasColumnType("int");

                    b.Property<int>("TypeVaccin")
                        .HasColumnType("int");

                    b.HasKey("VaccinId");

                    b.HasIndex("CentreVaccinationId");

                    b.ToTable("Vaccins");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Citoyen", b =>
                {
                    b.OwnsOne("Examen.ApplicationCore.Domain.Adresse", "Adresse", b1 =>
                        {
                            b1.Property<string>("CitoyenCIN")
                                .HasColumnType("nvarchar(450)");

                            b1.Property<int>("CodePostal")
                                .HasColumnType("int");

                            b1.Property<int>("Rue")
                                .HasColumnType("int");

                            b1.Property<string>("Ville")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("CitoyenCIN");

                            b1.ToTable("Citoyens");

                            b1.WithOwner()
                                .HasForeignKey("CitoyenCIN");
                        });

                    b.Navigation("Adresse")
                        .IsRequired();
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.RendezVous", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Citoyen", "Citoyen")
                        .WithMany("RendezVous")
                        .HasForeignKey("CitoyenId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Examen.ApplicationCore.Domain.Vaccin", "Vaccin")
                        .WithMany("RendezVous")
                        .HasForeignKey("VaccinId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Citoyen");

                    b.Navigation("Vaccin");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Vaccin", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.CentreVaccination", "CentreVaccination")
                        .WithMany("Vaccins")
                        .HasForeignKey("CentreVaccinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CentreVaccination");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.CentreVaccination", b =>
                {
                    b.Navigation("Vaccins");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Citoyen", b =>
                {
                    b.Navigation("RendezVous");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Vaccin", b =>
                {
                    b.Navigation("RendezVous");
                });
#pragma warning restore 612, 618
        }
    }
}
