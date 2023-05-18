using Examen.ApplicationCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen.Infrastructure.Configurations
{
    class CategorieConfiguration : IEntityTypeConfiguration<Categorie>
    {
        public void Configure(EntityTypeBuilder<Categorie> builder)
        {
            builder.HasMany(e => e.Livres).WithOne(e => e.Categorie).HasForeignKey(e => e.CategorieId);
        }
    }
}
