using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_KennyAPI.Migrations
{
    public partial class SeedVillaTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreateDate", "Details", "Name", "Occupancy", "Rate", "Sqft", "UpdateDate", "imageUrl" },
                values: new object[] { 1, "", new DateTime(2023, 7, 26, 21, 40, 53, 271, DateTimeKind.Local).AddTicks(6321), "Fine environment with good background", "Royal Villa", 12, 100.0, 300, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreateDate", "Details", "Name", "Occupancy", "Rate", "Sqft", "UpdateDate", "imageUrl" },
                values: new object[] { 2, "", new DateTime(2023, 7, 26, 21, 40, 53, 271, DateTimeKind.Local).AddTicks(6332), "Beautiful environment with swimming pool", "Diamon Villa", 15, 200.0, 400, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });

            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenity", "CreateDate", "Details", "Name", "Occupancy", "Rate", "Sqft", "UpdateDate", "imageUrl" },
                values: new object[] { 3, "", new DateTime(2023, 7, 26, 21, 40, 53, 271, DateTimeKind.Local).AddTicks(6333), "Beautiful garden", "De Villa", 5, 300.0, 100, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
