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
    public class PretLivreConfig : IEntityTypeConfiguration<PretLivre>
    {
        public void Configure(EntityTypeBuilder<PretLivre> builder)
        {
            builder.HasKey(t => new
            {
                t.LivreFk,
                t.AbonneFk,
                t.DateDebut
            }
           );
        }
    }
}
