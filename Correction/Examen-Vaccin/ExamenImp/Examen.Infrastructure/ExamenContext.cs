using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Infrastructure
{
    public class ExamenContext:DbContext
    {
        public DbSet<Citoyen> citoyens { get; set; }
        public DbSet<Vaccin> vaccins { get; set; }
        public DbSet<RendezVous> rendezVous { get; set; }
        public DbSet<CentreVaccination> centreVaccinations { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies(); 

            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                       Initial Catalog=ExamenVaccin;
                       Integrated Security=true;MultipleActiveResultSets=true");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RendezVousConfiguration());
            modelBuilder.Entity<Citoyen>().OwnsOne(c => c.Adresse, ad =>
            {
                ad.Property(a => a.CodePostal);
                ad.Property(a => a.Rue);
                ad.Property(a => a.Ville);
            });
        }
    }
}
