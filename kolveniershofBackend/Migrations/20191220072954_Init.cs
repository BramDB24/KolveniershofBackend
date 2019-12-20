using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kolveniershofBackend.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ateliers",
                columns: table => new
                {
                    AtelierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AtelierType = table.Column<int>(nullable: false),
                    Naam = table.Column<string>(nullable: false),
                    PictoURL = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ateliers", x => x.AtelierId);
                });

            migrationBuilder.CreateTable(
                name: "IdentityUserClaim<string>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: true),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUserClaim<string>", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opmerkingen",
                columns: table => new
                {
                    OpmerkingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tekst = table.Column<string>(nullable: true),
                    Datum = table.Column<DateTime>(nullable: false),
                    OpmerkingType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opmerkingen", x => x.OpmerkingId);
                });

            migrationBuilder.CreateTable(
                name: "Templates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naam = table.Column<string>(nullable: false),
                    IsActief = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Templates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruikers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: true),
                    Voornaam = table.Column<string>(nullable: false),
                    Achternaam = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Sfeergroep = table.Column<int>(nullable: false),
                    Foto = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruikers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gebruikers_IdentityUserClaim<string>_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IdentityUserClaim<string>",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DagPlanningen",
                columns: table => new
                {
                    DagplanningId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsTemplate = table.Column<bool>(nullable: false),
                    Weeknummer = table.Column<int>(nullable: false),
                    Eten = table.Column<string>(nullable: false),
                    Weekdag = table.Column<int>(nullable: false),
                    TemplateId = table.Column<int>(nullable: true),
                    Datum = table.Column<DateTime>(type: "Date", nullable: true),
                    Commentaar = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DagPlanningen", x => x.DagplanningId);
                    table.ForeignKey(
                        name: "FK_DagPlanningen_Templates_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Templates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Commentaar",
                columns: table => new
                {
                    CommentaarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Datum = table.Column<DateTime>(nullable: false),
                    GebruikerId = table.Column<string>(nullable: false),
                    CommentaarType = table.Column<int>(nullable: false),
                    Tekst = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commentaar", x => x.CommentaarId);
                    table.ForeignKey(
                        name: "FK_Commentaar_Gebruikers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DagAtelier",
                columns: table => new
                {
                    DagAtelierId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DagMoment = table.Column<int>(nullable: false),
                    AtelierId = table.Column<int>(nullable: true),
                    DagPlanningTemplateDagplanningId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DagAtelier", x => x.DagAtelierId);
                    table.ForeignKey(
                        name: "FK_DagAtelier_Ateliers_AtelierId",
                        column: x => x.AtelierId,
                        principalTable: "Ateliers",
                        principalColumn: "AtelierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DagAtelier_DagPlanningen_DagPlanningTemplateDagplanningId",
                        column: x => x.DagPlanningTemplateDagplanningId,
                        principalTable: "DagPlanningen",
                        principalColumn: "DagplanningId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GebruikerDagAtelier",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DagAtelierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GebruikerDagAtelier", x => new { x.DagAtelierId, x.Id });
                    table.ForeignKey(
                        name: "FK_GebruikerDagAtelier_DagAtelier_DagAtelierId",
                        column: x => x.DagAtelierId,
                        principalTable: "DagAtelier",
                        principalColumn: "DagAtelierId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GebruikerDagAtelier_Gebruikers_Id",
                        column: x => x.Id,
                        principalTable: "Gebruikers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commentaar_GebruikerId",
                table: "Commentaar",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DagAtelier_AtelierId",
                table: "DagAtelier",
                column: "AtelierId");

            migrationBuilder.CreateIndex(
                name: "IX_DagAtelier_DagPlanningTemplateDagplanningId",
                table: "DagAtelier",
                column: "DagPlanningTemplateDagplanningId");

            migrationBuilder.CreateIndex(
                name: "IX_DagPlanningen_TemplateId",
                table: "DagPlanningen",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_GebruikerDagAtelier_Id",
                table: "GebruikerDagAtelier",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Gebruikers_RoleId",
                table: "Gebruikers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_Naam",
                table: "Templates",
                column: "Naam",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Commentaar");

            migrationBuilder.DropTable(
                name: "GebruikerDagAtelier");

            migrationBuilder.DropTable(
                name: "Opmerkingen");

            migrationBuilder.DropTable(
                name: "DagAtelier");

            migrationBuilder.DropTable(
                name: "Gebruikers");

            migrationBuilder.DropTable(
                name: "Ateliers");

            migrationBuilder.DropTable(
                name: "DagPlanningen");

            migrationBuilder.DropTable(
                name: "IdentityUserClaim<string>");

            migrationBuilder.DropTable(
                name: "Templates");
        }
    }
}
