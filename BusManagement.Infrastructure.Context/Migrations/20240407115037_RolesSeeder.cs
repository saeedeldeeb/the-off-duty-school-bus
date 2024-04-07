using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BusManagement.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0de8240e-0bfc-492d-9758-d041c1314812", "678e1b6a-9a0c-4fdd-8bca-130ee693e2ae", "CompanyTransportationManager", "COMPANYTRANSPORTATIONMANAGER" },
                    { "fcdb4a01-a1be-4d5b-92e4-08b1163f47c7", "217ca5d6-29ce-4c73-8b92-de50c09f97f0", "SchoolTransportationManager", "SCHOOLTRANSPORTATIONMANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0de8240e-0bfc-492d-9758-d041c1314812");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fcdb4a01-a1be-4d5b-92e4-08b1163f47c7");
        }
    }
}
