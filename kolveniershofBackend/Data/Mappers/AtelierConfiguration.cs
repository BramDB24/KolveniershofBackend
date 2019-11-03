using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class AtelierConfiguration : IEntityTypeConfiguration<Atelier>
    {
        public void Configure(EntityTypeBuilder<Atelier> builder)
        {
            builder.HasKey(a => a.AtelierId);
            builder.Property(a => a.AtelierId).ValueGeneratedOnAdd();
            builder.Property(a => a.Naam).IsRequired();
            builder.Property(a => a.PictoURL).IsRequired();
            builder.Property(a => a.AtelierType).IsRequired();
            //atelier kent haar dagateliers
            builder.HasMany(a => a.DagAteliers).WithOne(da=>da.Atelier).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
