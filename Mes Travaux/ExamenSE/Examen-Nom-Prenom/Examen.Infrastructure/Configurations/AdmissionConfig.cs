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
    public class AdmissionConfig : IEntityTypeConfiguration<Admission>
    {
        public void Configure(EntityTypeBuilder<Admission> builder)
        {
            builder.HasOne(a => a.Patient)
                .WithMany(a=> a.Admissions)
                .HasForeignKey(a => a.PatientFk)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(a => a.Chambre)
                .WithMany(a=> a.Admissions)
                .HasForeignKey(a=> a.ChambreFK)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasKey(p => new
            {

                p.PatientFk,
                p.ChambreFK,
                p.DateAdmission
            });

        }
    }
}
