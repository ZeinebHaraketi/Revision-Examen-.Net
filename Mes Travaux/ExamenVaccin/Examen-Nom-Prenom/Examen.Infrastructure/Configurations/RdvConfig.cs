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
    public class RdvConfig : IEntityTypeConfiguration<RendezVous>
    {
        public void Configure(EntityTypeBuilder<RendezVous> builder)
        {
            //3-a
            // config de 1-* compte et transaction

            builder.HasOne(p => p.Citoyen)
                .WithMany(p => p.RendezVous)
                .HasForeignKey(p => p.CitoyenId)
                .OnDelete(DeleteBehavior.Restrict);

            //config de 1 * DAB et Transaction


            builder.HasOne(p => p.Vaccin)
                    .WithMany(p => p.RendezVous)
                    .HasForeignKey(p => p.VaccinId)
                    .OnDelete(DeleteBehavior.Restrict);

            //3-b  // Clé primaire composée (porteuse)
            builder.HasKey(p => new
            {

                p.CitoyenId,
                p.VaccinId,
                p.DateVaccination
            });
        }
    }
}
