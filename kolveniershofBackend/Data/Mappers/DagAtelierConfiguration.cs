using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class DagAtelierConfiguration : IEntityTypeConfiguration<DagAtelier>
    {
        public void Configure(EntityTypeBuilder<DagAtelier> builder)
        {
            builder.HasKey(da => da.DagAtelierId);
            builder.Property(da => da.DagAtelierId).ValueGeneratedOnAdd();
            builder.Property(da => da.DagMoment).IsRequired();

            //dagatelier kent haar atelier
            builder.HasOne(da => da.Atelier).WithMany(a => a.DagAteliers);
        }
    }
}
