using Examen.ApplicationCore.Domain;
using Examen.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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
        public DbSet<Banque> Banques { get; set; }
        public DbSet<Compte> Comptes { get; set; }
        public DbSet<DAB> DABs { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionRetrait> TransactionRetraits { get; set; }
        public DbSet<TransactionTransfert> TransactionTransferts { get; set; }






        //....................
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\mssqllocaldb;
                                          Initial Catalog=DABZeinebHaraketi;
                                          Integrated Security=true;
                                          MultipleActiveResultSets=true");
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseLazyLoadingProxies(); //activer lazy loading
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ExempleConfiguration());
            modelBuilder.ApplyConfiguration(new TransationConfig());

            //...................
            //tpt 
            //tph => config

            //Foreign Key lel Transaction
            modelBuilder.Entity<Transaction>()
                .HasOne(t=> t.Compte)
                .WithMany(t=> t.Transactions)
                .HasForeignKey(t=> t.CompteFk)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Transaction>()
                .HasOne(t=> t.DAB)
                .WithMany(t=> t.Transactions)
                .HasForeignKey(t=> t.DabFK) 
                .OnDelete(DeleteBehavior.Restrict);

            //TPT
            modelBuilder.Entity<TransactionRetrait>().ToTable("TransactionRetrait");
            modelBuilder.Entity<TransactionTransfert>().ToTable("TransactionTransfert");



        }
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //configurationBuilder.Properties<DateTime>().HaveColumnType("date");
        }
    }
}
