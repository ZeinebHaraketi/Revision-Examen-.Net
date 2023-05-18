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
    public class ChambreConfig : IEntityTypeConfiguration<Chambre>
    {
        public void Configure(EntityTypeBuilder<Chambre> builder)
        {
            builder.HasOne(t => t.Clinique)
                .WithMany(t=> t.Chambres)
                .HasForeignKey(t=> t.CliniqueFK)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
