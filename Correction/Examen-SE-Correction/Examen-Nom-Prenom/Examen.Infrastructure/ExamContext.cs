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
        public DbSet<Admission> Admissions { get; set; }
        public DbSet<Chambre> Chambres { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinique> Cliniques { get; set; }

        //....................
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=AbirHbechaDB;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); //activer lazy loading
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CliniqueConfiguration());
           
            
            
            //config de type détenu
            modelBuilder.Entity<Patient>()
                .OwnsOne(p => p.NomComplet, NC =>
                   {
                       NC.Property(p => p.Nom);
                       NC.Property(p => p.Prenom);
                   });
            //config de la clé composée
            modelBuilder.Entity<Admission>()
                .HasKey(p => new
                {
                    p.ChambreFk,
                    p.PatientFk,
                    p.DateAdmission
                });


            //tpt 
            //tph => config
        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
           // configurationBuilder.Properties<string>().HaveMaxLength(50);
        }
    }
}
