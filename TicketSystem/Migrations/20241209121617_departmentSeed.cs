﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketSystem.Migrations
{
    /// <inheritdoc />
    public partial class departmentSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedAt", "Title" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 9, 14, 16, 16, 546, DateTimeKind.Local).AddTicks(6736), "Programming" },
                    { 2, new DateTime(2024, 12, 9, 14, 16, 16, 546, DateTimeKind.Local).AddTicks(6798), "Engineering" },
                    { 3, new DateTime(2024, 12, 9, 14, 16, 16, 546, DateTimeKind.Local).AddTicks(6805), "CallCenter" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
