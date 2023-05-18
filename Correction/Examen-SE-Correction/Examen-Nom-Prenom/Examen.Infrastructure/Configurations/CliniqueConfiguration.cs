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
    public class CliniqueConfiguration : IEntityTypeConfiguration<Clinique>
    {
        public void Configure(EntityTypeBuilder<Clinique> builder)
        {
            // config de 1-* cliniqueFK
            builder.HasMany(p => p.Chambres)
                .WithOne(p => p.Clinique)
                .HasForeignKey(p => p.CliniqueFk)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
