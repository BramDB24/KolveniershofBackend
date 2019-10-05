using kolveniershofBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<DagPlanning> Dagplanningen { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //gebruikers
            builder.Entity<Gebruiker>();
            builder.Entity<Gebruiker>().Property(r => r.Voornaam).IsRequired().HasMaxLength(50);

            //dagplanningen
            builder.Entity<DagPlanning>();
            builder.Entity<DagPlanning>().HasKey(d => d.Id);
            //builder.Entity<DagPlanning>().HasMany(d => d.DagAteliers).WithOne().OnDelete(DeleteBehavior.Cascade);
        }

    }
}
