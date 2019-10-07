using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class DagPlanningConfiguration : IEntityTypeConfiguration<DagPlanning>
    {
        public void Configure(EntityTypeBuilder<DagPlanning> builder)
        {
            builder.Property(dpt => dpt.Weekdag).IsRequired();
            builder.Property(dpt => dpt.Eten).IsRequired();

            //dagplanning heeft een lijst van opmerkingen
            builder.HasMany(d => d.Opmerkingen).WithOne().OnDelete(DeleteBehavior.Cascade);

        }
    }
}
