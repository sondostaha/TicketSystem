using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class userSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "53dfae71-e5f0-4ae6-bc3c-38716ce04899", "d9c62d32-b1bc-4a7c-8807-c119b3771040", "Employee", "employee" },
                    { "6db4f567-4ccb-40af-af09-0e846100f520", "9109af82-a31b-4feb-8670-38b5b9c32646", "Admin", "admin" },
                    { "bae3d0cd-0785-49e0-a479-abb9a4b5c6ae", "08942456-da84-4226-a214-ec220d751600", "Director", "director" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AssocBranch", "ConcurrencyStamp", "DepartmentId", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ac73ab2c-572f-4948-a2f0-2bc4371bdb86", 0, 1, "548ab2e0-d129-4799-8cb5-0dc040640ca9", 1, "Users", "sondos@gmail.com", true, false, null, "Sondos@gmail.com", "Sondos", "AQAAAAIAAYagAAAAEFyv9E2RsBfLJbQNy+dFvYeh1t8e3L5ejnrlmzGcNQM4Qi1XO5kpaUVRnsaaQPfYWg==", null, false, "e8215846-c8a7-4239-97fb-6ca49dca89a6", (byte)1, false, "sondos" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 42, 29, 299, DateTimeKind.Local).AddTicks(2494));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 42, 29, 299, DateTimeKind.Local).AddTicks(2549));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 9, 14, 42, 29, 299, DateTimeKind.Local).AddTicks(2553));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "6db4f567-4ccb-40af-af09-0e846100f520", "ac73ab2c-572f-4948-a2f0-2bc4371bdb86" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "53dfae71-e5f0-4ae6-bc3c-38716ce04899");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bae3d0cd-0785-49e0-a479-abb9a4b5c6ae");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6db4f567-4ccb-40af-af09-0e846100f520", "ac73ab2c-572f-4948-a2f0-2bc4371bdb86" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6db4f567-4ccb-40af-af09-0e846100f520");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ac73ab2c-572f-4948-a2f0-2bc4371bdb86");

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
    }
}
