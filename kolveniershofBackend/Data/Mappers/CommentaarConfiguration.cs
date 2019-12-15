using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data.Mappers
{
    public class CommentaarConfiguration : IEntityTypeConfiguration<Commentaar>
    {
        public void Configure(EntityTypeBuilder<Commentaar> builder)
        {
            builder.HasKey(c => c.CommentaarId);
            //builder.Property(c => c.CommentaarId).ValueGeneratedOnAdd();
            builder.Property(c => c.Datum).IsRequired();
            builder.Property(c => c.CommentaarType).IsRequired();
            builder.Property(c => c.Tekst).IsRequired();
            builder.Property(c => c.GebruikerId).IsRequired();
        }
    }
}
