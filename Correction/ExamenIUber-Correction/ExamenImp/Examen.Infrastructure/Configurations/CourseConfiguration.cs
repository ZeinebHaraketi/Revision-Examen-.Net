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
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasOne(c => c.Client).WithMany(c => c.Courses).HasForeignKey(c => c.ClientId);
            builder.HasOne(c => c.Voiture).WithMany(c => c.Courses).HasForeignKey(c => c.VoitureId);
            builder.HasKey(c => new { c.DateCourse, c.VoitureId, c.ClientId });
        }
    }
}
