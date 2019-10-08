using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class GebruikerConfiguration : IEntityTypeConfiguration<Gebruiker>
    {
        public void Configure(EntityTypeBuilder<Gebruiker> builder)
        {
            builder.Property(g => g.Achternaam).IsRequired();
            builder.Property(g => g.Voornaam).IsRequired();
            builder.Property(g => g.Email).IsRequired();
            builder.Property(g => g.Gemeente).IsRequired();
            builder.Property(g => g.Postcode).IsRequired();
            builder.Property(g => g.Straatnaam).IsRequired();
            builder.Property(g => g.Huisnummer).IsRequired();
            builder.Property(g => g.Sfeergroep).IsRequired();
            builder.Property(g => g.Foto).IsRequired();
            builder.Property(g => g.Type).IsRequired();

            //gebruiker kent zijn / haar commentaren
            builder.HasMany(g=>g.Commentaren).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}
