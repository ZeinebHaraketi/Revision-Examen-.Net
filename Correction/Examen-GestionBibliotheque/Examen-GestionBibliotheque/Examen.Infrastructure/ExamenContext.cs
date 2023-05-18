using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examen.Infrastructure
{
    public class ExamenContext : DbContext
    {
        public DbSet<Abonne> Abonnes { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Livre> Livre { get; set; }
        public DbSet<PretLivre> PretLivres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
              Initial Catalog=GestionLivreDB;Integrated Security=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PretLivreConfiguration());
            modelBuilder.ApplyConfiguration(new CategorieConfiguration());
        }
    }
}
