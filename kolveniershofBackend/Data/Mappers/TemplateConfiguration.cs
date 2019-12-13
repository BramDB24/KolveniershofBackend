using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kolveniershofBackend.Data.Mappers
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(t => t.Naam).IsUnique();
            builder.Property(t => t.Naam).IsRequired();
            builder.Property(t => t.IsActief).IsRequired();
            builder.HasMany(t => t.DagPlanningTemplates).WithOne().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
