using System;
using System.Collections.Generic;
using System.Text;
using Examen.ApplicationCore.Domain;

//
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Examen.Infrastructure
{
    public class RendezVousConfiguration : IEntityTypeConfiguration<RendezVous>
    {
        public void Configure(EntityTypeBuilder<RendezVous> builder)
        {
            builder.HasKey(f => new { f.VaccinId, f.CitoyenId, f.DateVaccination });//clé primaire compose

            // Configurer la cle etrangere
            builder.HasOne<Citoyen>(f => f.citoyen)
                .WithMany(p => p.rendezVous)
                .HasForeignKey(f => f.CitoyenId);

            // Configurer la cle etrangere
            builder.HasOne<Vaccin>(f => f.vaccin)
           .WithMany(p => p.rendezVous)
           .HasForeignKey(f => f.VaccinId);
        }
    }
}
