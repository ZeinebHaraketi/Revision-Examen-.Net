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
        public DbSet<Abonne> Abonnes { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<Livre> Livres { get; set; }
        public DbSet<PretLivre> PretLivres { get; set; }




        //....................
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=BibliothequeZeinebHaraketi;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); //activer lazy loading
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExempleConfiguration());
            modelBuilder.ApplyConfiguration(new PretLivreConfig());
            modelBuilder.ApplyConfiguration(new CategorieConfig());


            //...................
            //tpt 
            //tph => config
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }
    }
}
