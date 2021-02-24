using Microsoft.EntityFrameworkCore.Migrations;

namespace Fooder.Persistence.Migrations
{
    public partial class PetAfflictionMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PetAffliction",
                columns: table => new
                {
                    PetEntityId = table.Column<int>(nullable: false),
                    AfflictionEntityId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PetAffliction", x => new { x.AfflictionEntityId, x.PetEntityId });
                    table.ForeignKey(
                        name: "FK_PetAffliction_Afflictions_AfflictionEntityId",
                        column: x => x.AfflictionEntityId,
                        principalTable: "Afflictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PetAffliction_Pets_PetEntityId",
                        column: x => x.PetEntityId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PetAffliction_PetEntityId",
                table: "PetAffliction",
                column: "PetEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PetAffliction");
        }
    }
}
