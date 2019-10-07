using kolveniershofBackend.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolveniershofBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<DagPlanningTemplate> Dagplanningen { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //gebruikers
            builder.Entity<Gebruiker>().ToTable("Gebruiker");
            builder.Entity<Gebruiker>();
            builder.Entity<Gebruiker>().Property(r => r.Voornaam).IsRequired().HasMaxLength(50);

            //dagplanningTemplates
            builder.Entity<DagPlanningTemplate>().ToTable("Dagplanning");
            builder.Entity<DagPlanningTemplate>().HasKey(d => d.Id);
            builder.Entity<DagPlanningTemplate>().HasMany(d => d.DagAteliers).WithOne().OnDelete(DeleteBehavior.Cascade);
            builder.Entity<DagPlanningTemplate>().HasDiscriminator<bool>("IsTemplate")
                .HasValue<DagPlanning>(false)
                .HasValue<DagPlanningTemplate>(true);
            //dagplanningen
            builder.Entity<DagPlanning>().HasMany(d => d.Opmerkingen).WithOne().OnDelete(DeleteBehavior.Cascade);
            //builder.Entity<DagPlanning>().HasMany(d => d.DagAteliers).WithOne().OnDelete(DeleteBehavior.Cascade);
        }

    }
}
