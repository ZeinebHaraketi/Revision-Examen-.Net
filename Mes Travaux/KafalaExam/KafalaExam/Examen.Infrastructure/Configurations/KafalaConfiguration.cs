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
    public class KafalaConfiguration : IEntityTypeConfiguration<Kafala>
    {
        public void Configure(EntityTypeBuilder<Kafala> builder)
        {
            builder.HasKey(r => new
            {
                r.BeneficiaryFk,
                r.DonatorFk,
                r.StartDate,
                r.EndDate
            });
        }
    }
}
