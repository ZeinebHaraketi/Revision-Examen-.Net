using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.Infrastructure.Configurations
{
    public class ProduitConfig : IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
            // ManyToMany Configuration
            builder.HasMany(p => p.Fournisseurs)
                .WithMany(f => f.Produits)
                .UsingEntity(e => e.ToTable("Facture"));
        }
    }
}
