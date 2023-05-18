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
       // public DbSet<Exemple> Exemples { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<Donator> Donators { get; set; }
        public DbSet<Kafala> Kafalas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=SeifEXAM;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); //activer lazy loading
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExempleConfiguration());
            modelBuilder.ApplyConfiguration(new KafalaConfiguration());

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
