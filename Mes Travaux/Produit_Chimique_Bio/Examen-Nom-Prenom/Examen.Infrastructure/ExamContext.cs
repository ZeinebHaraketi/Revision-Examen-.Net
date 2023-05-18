using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Infrastructure
{
    public class ExamContext:DbContext
    {
        //les dbsets
        public DbSet<Exemple> Exemples { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<Fournisseur> Fournisseurs { get; set; }

        public DbSet<Chimique> Chimiques { get; set; }

        public DbSet<Biologique> Biologiques { get; set; }

        public DbSet<Categorie> Categories { get; set; }





        //....................
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=ZeinebHaraketiProduit;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); //activer lazy loading
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExempleConfiguration());
            modelBuilder.ApplyConfiguration(new ProduitConfig());

            //...................
            //tpt 
            //tph => config
            /* modelBuilder.Entity<Produit>()
                 .HasMany(p => p.Fournisseurs)
                 .WithMany(p => p.Produits)
                 .UsingEntity(p => p.ToTable("Facture"));*/

            modelBuilder.Entity<Produit>()
                .HasDiscriminator<string>("TypeProduit")
                .HasValue<Chimique>("C")
                .HasValue<Biologique>("B")
                .HasValue<Produit>("P");
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
            configurationBuilder.Properties<string>().HaveMaxLength(50);
        }
    }
}
