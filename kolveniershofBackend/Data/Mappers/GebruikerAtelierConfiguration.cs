using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kolveniershofBackend.Data.Mappers
{
    public class GebruikerAtelierConfiguration : IEntityTypeConfiguration<GebruikerDagAtelier>
    {
        public void Configure(EntityTypeBuilder<GebruikerDagAtelier> builder)
        {

            builder.HasKey(ga => new { ga.DagAtelierId, ga.Id });

            builder.HasOne(ga => ga.DagAtelier)
                .WithMany(da => da.GebruikerDagAteliers)
                .HasForeignKey(ga => ga.DagAtelierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ga => ga.Gebruiker)
                .WithMany()
                .HasForeignKey(ga => ga.Id)
                .OnDelete(DeleteBehavior.Cascade);





        }
    }
}
