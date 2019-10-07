using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class OpmerkingConfiguration : IEntityTypeConfiguration<Opmerking>
    {
        public void Configure(EntityTypeBuilder<Opmerking> builder)
        {
            builder.HasKey(o => o.OpmerkingId);
            builder.Property(o => o.OpmerkingId).ValueGeneratedOnAdd();
            builder.Property(o => o.OpmerkingType).IsRequired();
            builder.Property(o => o.Tekst).IsRequired();
        }
    }
}
