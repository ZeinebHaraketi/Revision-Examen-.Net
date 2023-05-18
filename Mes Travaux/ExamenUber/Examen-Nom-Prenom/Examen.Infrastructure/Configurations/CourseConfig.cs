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
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(p => p.Voiture)
               .WithMany(p => p.Courses)
               .HasForeignKey(p => p.VoitureId)
               .OnDelete(DeleteBehavior.Restrict);

            //config de 1 * DAB et Transaction


            builder.HasOne(p => p.Client)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(p => p.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

            //3-b  // Clé primaire composée (porteuse)
            builder.HasKey(p => new
            {

                p.VoitureId,
                p.ClientId,
                p.DateCourse
            });
        }
    }
}
