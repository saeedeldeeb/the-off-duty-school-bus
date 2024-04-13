using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusManagement.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedVehicleBrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "VehicleBrands");

            migrationBuilder.CreateTable(
                name: "VehicleBrandTranslation",
                columns: table => new
                {
                    VehicleBrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleBrandTranslation", x => new { x.VehicleBrandId, x.Language });
                    table.ForeignKey(
                        name: "FK_VehicleBrandTranslation_VehicleBrands_VehicleBrandId",
                        column: x => x.VehicleBrandId,
                        principalTable: "VehicleBrands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VehicleBrandTranslation");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "VehicleBrands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
