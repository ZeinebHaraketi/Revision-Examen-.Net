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
    public class ExempleConfiguration : IEntityTypeConfiguration<Exemple>
    {
        public void Configure(EntityTypeBuilder<Exemple> builder)
        {
            // builder.
            //.............
        }
    }
}
