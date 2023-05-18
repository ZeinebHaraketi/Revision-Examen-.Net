using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen.Infrastructure.Configurations
{
    class PretLivreConfiguration : IEntityTypeConfiguration<PretLivre>
    {
        public void Configure(EntityTypeBuilder<PretLivre> builder)
        {
            builder.HasKey(e => new { e.LivreFK, e.AbonneFK, e.DateDebut });

            builder.HasOne(e => e.Livre).WithMany(e => e.PretLivres).HasForeignKey(e => e.LivreFK).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Abonne).WithMany(e => e.PretLivres).HasForeignKey(e => e.AbonneFK);
        }
    }
}
