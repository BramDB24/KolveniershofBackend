using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class DagPlanningTemplateConfiguration : IEntityTypeConfiguration<DagPlanningTemplate>
    {
        public void Configure(EntityTypeBuilder<DagPlanningTemplate> builder)
        {
            builder.HasKey(dpt => dpt.DagplanningId);
            builder.Property(dpt => dpt.DagplanningId).ValueGeneratedOnAdd();
            builder.Property(dpt => dpt.IsTemplate).IsRequired();
            builder.Property(dpt => dpt.Weeknummer).IsRequired();
            
            //dagplanning heeft een lijst van dagateliers
            builder.HasMany(d => d.DagAteliers).WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasDiscriminator<bool>("IsTemplate")
                .HasValue<DagPlanning>(false)
                .HasValue<DagPlanningTemplate>(true);
        }
    }
}
