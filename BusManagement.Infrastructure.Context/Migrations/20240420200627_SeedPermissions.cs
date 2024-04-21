using BusManagement.Core.Common.Constants;
using BusManagement.Core.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusManagement.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // seed school transportation manager permissions
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "RoleId", "ClaimType", "ClaimValue" },
                values: Permissions.GeneratePermissionsDataForSchoolTransportationManager()
            );

            // seed company transportation manager permissions
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "RoleId", "ClaimType", "ClaimValue" },
                values: Permissions.GeneratePermissionsDataForCompanyTransportationManager()
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("AspNetRoleClaims", "ClaimType", "Permission");
        }
    }
}
