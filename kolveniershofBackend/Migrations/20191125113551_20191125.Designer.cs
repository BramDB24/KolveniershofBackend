﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using kolveniershofBackend.Data;

namespace kolveniershofBackend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20191125113551_20191125")]
    partial class _20191125
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.ToTable("IdentityUserClaim<string>");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.Atelier", b =>
                {
                    b.Property<int>("AtelierId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AtelierType");

                    b.Property<string>("Naam")
                        .IsRequired();

                    b.Property<string>("PictoURL")
                        .IsRequired();

                    b.HasKey("AtelierId");

                    b.ToTable("Ateliers");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.Commentaar", b =>
                {
                    b.Property<int>("CommentaarId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CommentaarType");

                    b.Property<DateTime>("Datum");

                    b.Property<string>("GebruikerId");

                    b.Property<string>("Tekst")
                        .IsRequired();

                    b.HasKey("CommentaarId");

                    b.HasIndex("GebruikerId");

                    b.ToTable("Commentaar");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.DagAtelier", b =>
                {
                    b.Property<int>("DagAtelierId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AtelierId");

                    b.Property<int>("DagMoment");

                    b.Property<int?>("DagPlanningTemplateDagplanningId");

                    b.HasKey("DagAtelierId");

                    b.HasIndex("AtelierId");

                    b.HasIndex("DagPlanningTemplateDagplanningId");

                    b.ToTable("DagAtelier");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.DagPlanningTemplate", b =>
                {
                    b.Property<int>("DagplanningId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsTemplate");

                    b.Property<int>("Weekdag");

                    b.Property<int>("Weeknummer");

                    b.HasKey("DagplanningId");

                    b.ToTable("DagPlanningen");

                    b.HasDiscriminator<bool>("IsTemplate").HasValue(true);
                });

            modelBuilder.Entity("kolveniershofBackend.Models.Gebruiker", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Achternaam")
                        .IsRequired();

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Foto")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int?>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<int>("Sfeergroep");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<int>("Type");

                    b.Property<string>("UserName");

                    b.Property<string>("Voornaam")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Gebruikers");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.GebruikerDagAtelier", b =>
                {
                    b.Property<int>("DagAtelierId");

                    b.Property<string>("Id");

                    b.HasKey("DagAtelierId", "Id");

                    b.HasIndex("Id");

                    b.ToTable("GebruikerDagAtelier");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.Opmerking", b =>
                {
                    b.Property<int>("OpmerkingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Datum");

                    b.Property<int>("OpmerkingType");

                    b.Property<string>("Tekst");

                    b.HasKey("OpmerkingId");

                    b.ToTable("Opmerkingen");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.DagPlanning", b =>
                {
                    b.HasBaseType("kolveniershofBackend.Models.DagPlanningTemplate");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("Date");

                    b.Property<string>("Eten")
                        .IsRequired();

                    b.HasDiscriminator().HasValue(false);
                });

            modelBuilder.Entity("kolveniershofBackend.Models.Commentaar", b =>
                {
                    b.HasOne("kolveniershofBackend.Models.Gebruiker")
                        .WithMany("Commentaren")
                        .HasForeignKey("GebruikerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("kolveniershofBackend.Models.DagAtelier", b =>
                {
                    b.HasOne("kolveniershofBackend.Models.Atelier", "Atelier")
                        .WithMany("DagAteliers")
                        .HasForeignKey("AtelierId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("kolveniershofBackend.Models.DagPlanningTemplate")
                        .WithMany("DagAteliers")
                        .HasForeignKey("DagPlanningTemplateDagplanningId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("kolveniershofBackend.Models.Gebruiker", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("kolveniershofBackend.Models.GebruikerDagAtelier", b =>
                {
                    b.HasOne("kolveniershofBackend.Models.DagAtelier", "DagAtelier")
                        .WithMany("Gebruikers")
                        .HasForeignKey("DagAtelierId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("kolveniershofBackend.Models.Gebruiker", "Gebruiker")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
