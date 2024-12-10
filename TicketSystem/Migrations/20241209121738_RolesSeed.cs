using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class RolesSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d62c1e5e-7a52-4a77-817e-a39f95d2f0ce", "5fdde62b-1be2-42d3-bb2e-21c0546c5b62", "Admin", "admin" },
                    { "dfe2fb69-7b70-47f6-96e9-d356dbe81c08", "0e7515c3-d1c0-4dda-a4f3-52a8de6f9051", "Employee", "employee" },
                    { "e25c3bc9-5aa4-4a21-936d-444eba761789", "abc882ab-bd87-4743-96b9-4205f6c6a011", "Director", "director" }
                });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 17, 37, 539, DateTimeKind.Local).AddTicks(1650));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 17, 37, 539, DateTimeKind.Local).AddTicks(1709));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 17, 37, 539, DateTimeKind.Local).AddTicks(1715));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d62c1e5e-7a52-4a77-817e-a39f95d2f0ce");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dfe2fb69-7b70-47f6-96e9-d356dbe81c08");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e25c3bc9-5aa4-4a21-936d-444eba761789");

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 16, 53, 71, DateTimeKind.Local).AddTicks(9809));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 16, 53, 71, DateTimeKind.Local).AddTicks(9863));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 16, 53, 71, DateTimeKind.Local).AddTicks(9869));
        }
    }
}
