using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class tickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProgressIndicators = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tickets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54066e0c-8591-418f-88df-d572f835d8e8", "0d6a6827-3a1e-4b76-a3e5-14cbaa5a8a1c", "Employee", "employee" },
                    { "698728fa-f4a7-43b2-a1cc-2bc91f129987", "b3117d13-a5e6-4931-aa52-9c3fd274dbd0", "Admin", "admin" },
                    { "8fe78d0f-da51-45d2-b109-20782e2c10c7", "a24d55ea-be98-4d40-88f0-ee3d8cb1e446", "Director", "director" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AssocBranch", "ConcurrencyStamp", "DepartmentId", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Status", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5cdfbbaa-5c0d-40c7-96bb-1a965009028c", 0, 1, "6430bdc6-77fa-4e12-bb88-109094dd1ef9", 1, "Users", "sondos@gmail.com", true, false, null, "Sondos@gmail.com", "Sondos", "AQAAAAIAAYagAAAAENrlxb2Zfzhkl2zEFM12yYjWx3p+Ji+uG0Pkt+RzxPXjbCccq6la21EP1fmaws1v3A==", null, false, "b2f806f6-d6a8-4219-b6da-7d952c157859", (byte)1, false, "sondos" });

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 10, 12, 16, 12, 89, DateTimeKind.Local).AddTicks(2084));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 10, 12, 16, 12, 89, DateTimeKind.Local).AddTicks(2137));

            migrationBuilder.UpdateData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2024, 12, 10, 12, 16, 12, 89, DateTimeKind.Local).AddTicks(2141));

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "698728fa-f4a7-43b2-a1cc-2bc91f129987", "5cdfbbaa-5c0d-40c7-96bb-1a965009028c" });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_CreatorId",
                table: "Tickets",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_UserId",
                table: "Tickets",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54066e0c-8591-418f-88df-d572f835d8e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8fe78d0f-da51-45d2-b109-20782e2c10c7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "698728fa-f4a7-43b2-a1cc-2bc91f129987", "5cdfbbaa-5c0d-40c7-96bb-1a965009028c" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "698728fa-f4a7-43b2-a1cc-2bc91f129987");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5cdfbbaa-5c0d-40c7-96bb-1a965009028c");

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
    }
}
