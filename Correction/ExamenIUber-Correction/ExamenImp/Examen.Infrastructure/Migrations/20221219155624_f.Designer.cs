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
    [DbContext(typeof(ExamenContext))]
    [Migration("20221219155624_f")]
    partial class f
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Chauffeur", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<float>("TauxBenefice")
                        .HasColumnType("real");

                    b.Property<string>("VoitureNumMat")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("VoitureNumMat");

                    b.ToTable("Chauffeurs");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Client", b =>
                {
                    b.Property<string>("CIN")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("DateNaissance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("CIN");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Course", b =>
                {
                    b.Property<DateTime>("DateCourse")
                        .HasColumnType("datetime2");

                    b.Property<string>("VoitureId")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ClientId")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("Etat")
                        .HasColumnType("int");

                    b.Property<string>("LieuArrivee")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LieuDepart")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("DateCourse", "VoitureId", "ClientId");

                    b.HasIndex("ClientId");

                    b.HasIndex("VoitureId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Voiture", b =>
                {
                    b.Property<string>("NumMat")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Couleur")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Marque")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("NumMat");

                    b.ToTable("Voitures");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Chauffeur", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Voiture", "Voiture")
                        .WithMany("Chauffeurs")
                        .HasForeignKey("VoitureNumMat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Voiture");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Course", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Client", "Client")
                        .WithMany("Courses")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examen.ApplicationCore.Domain.Voiture", "Voiture")
                        .WithMany("Courses")
                        .HasForeignKey("VoitureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Examen.ApplicationCore.Domain.Paiement", "Paiement", b1 =>
                        {
                            b1.Property<DateTime>("CourseDateCourse")
                                .HasColumnType("datetime2");

                            b1.Property<string>("CourseVoitureId")
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.Property<string>("CourseClientId")
                                .HasMaxLength(150)
                                .HasColumnType("nvarchar(150)");

                            b1.Property<double>("Montant")
                                .HasColumnType("float");

                            b1.Property<bool>("PaiementEnLigne")
                                .HasColumnType("bit");

                            b1.HasKey("CourseDateCourse", "CourseVoitureId", "CourseClientId");

                            b1.ToTable("Courses");

                            b1.WithOwner()
                                .HasForeignKey("CourseDateCourse", "CourseVoitureId", "CourseClientId");
                        });

                    b.Navigation("Client");

                    b.Navigation("Paiement")
                        .IsRequired();

                    b.Navigation("Voiture");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Client", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Voiture", b =>
                {
                    b.Navigation("Chauffeurs");

                    b.Navigation("Courses");
                });
#pragma warning restore 612, 618
        }
    }
}