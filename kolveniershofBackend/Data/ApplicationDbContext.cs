using kolveniershofBackend.Data.Mappers;
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
        public DbSet<Template> Templates { get; set; }
        public DbSet<DagPlanningTemplate> DagPlanningen { get; set; }
        public DbSet<Atelier> Ateliers { get; set; }
        public DbSet<Gebruiker> Gebruikers { get; set; }
        public DbSet<Commentaar> Commentaar { get; set; }

        public DbSet<Opmerking> Opmerkingen { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new AtelierConfiguration());
            builder.ApplyConfiguration(new CommentaarConfiguration());
            builder.ApplyConfiguration(new DagAtelierConfiguration());
            builder.ApplyConfiguration(new DagPlanningConfiguration());
            builder.ApplyConfiguration(new DagPlanningTemplateConfiguration());
            builder.ApplyConfiguration(new GebruikerConfiguration());
            builder.ApplyConfiguration(new OpmerkingConfiguration());
            builder.ApplyConfiguration(new GebruikerAtelierConfiguration());
            builder.ApplyConfiguration(new TemplateConfiguration());
        }
    }
}
