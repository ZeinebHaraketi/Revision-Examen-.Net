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
    public class RDVConfig : IEntityTypeConfiguration<RDV>
    {
        public void Configure(EntityTypeBuilder<RDV> builder)
        {
            //3-a
            // config de 1-* compte et transaction

            builder.HasOne(p => p.Client)
                .WithMany(p => p.RDVs)
                .HasForeignKey(p => p.ClientFK)
                .OnDelete(DeleteBehavior.Restrict);

            //config de 1 * DAB et Transaction


            builder.HasOne(p => p.Prestation)
                    .WithMany(p => p.RDVs)
                    .HasForeignKey(p => p.PrestationFK)
                    .OnDelete(DeleteBehavior.Restrict);

            //3-b  // Clé primaire composée (porteuse)
            builder.HasKey(p => new
            {

                p.PrestationFK,
                p.ClientFK,
                p.DateRDV
            });
        }
    }
}
