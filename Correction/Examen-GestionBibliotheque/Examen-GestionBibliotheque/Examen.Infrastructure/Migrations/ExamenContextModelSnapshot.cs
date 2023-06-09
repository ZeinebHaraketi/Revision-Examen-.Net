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
    [DbContext(typeof(ExamenContext))]
    partial class ExamenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Abonne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Adresse")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategorieCode")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Prenom")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Statut")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategorieCode");

                    b.ToTable("Abonnes");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Categorie", b =>
                {
                    b.Property<int>("Code")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Code"), 1L, 1);

                    b.Property<string>("Libelle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Code");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Livre", b =>
                {
                    b.Property<int>("LivreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LivreId"), 1L, 1);

                    b.Property<string>("Auteur")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategorieId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateCreation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NbExemplaires")
                        .HasColumnType("int");

                    b.Property<string>("Titre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LivreId");

                    b.HasIndex("CategorieId");

                    b.ToTable("Livre");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.PretLivre", b =>
                {
                    b.Property<int>("LivreFK")
                        .HasColumnType("int");

                    b.Property<int>("AbonneFK")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateDebut")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFin")
                        .HasColumnType("datetime2");

                    b.HasKey("LivreFK", "AbonneFK", "DateDebut");

                    b.HasIndex("AbonneFK");

                    b.ToTable("PretLivres");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Abonne", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Categorie", "Categorie")
                        .WithMany()
                        .HasForeignKey("CategorieCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Livre", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Categorie", "Categorie")
                        .WithMany("Livres")
                        .HasForeignKey("CategorieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorie");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.PretLivre", b =>
                {
                    b.HasOne("Examen.ApplicationCore.Domain.Abonne", "Abonne")
                        .WithMany("PretLivres")
                        .HasForeignKey("AbonneFK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Examen.ApplicationCore.Domain.Livre", "Livre")
                        .WithMany("PretLivres")
                        .HasForeignKey("LivreFK")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Abonne");

                    b.Navigation("Livre");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Abonne", b =>
                {
                    b.Navigation("PretLivres");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Categorie", b =>
                {
                    b.Navigation("Livres");
                });

            modelBuilder.Entity("Examen.ApplicationCore.Domain.Livre", b =>
                {
                    b.Navigation("PretLivres");
                });
#pragma warning restore 612, 618
        }
    }
}
