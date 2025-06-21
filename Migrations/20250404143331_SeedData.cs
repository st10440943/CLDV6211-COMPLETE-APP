using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CLDV6211PART_1_App.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Bookings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Venues",
                columns: new[] { "VenueID", "Capacity", "ImageUrl", "Location", "VenueName" },
                values: new object[,]
                {
                    { 1, 5000, "http://example.com/images/cascades_mall.jpg", "Cascades Boulevard, Pietermaritzburg, 3201", "Cascades Shopping Mall" },
                    { 2, 30000, "http://example.com/images/royal_showgrounds.jpg", "Royal Agricultural Showgrounds, Pietermaritzburg", "Royal Showgrounds" },
                    { 3, 1200, "http://example.com/images/city_hall.jpg", "City Hall, Pietermaritzburg, 3201", "Pietermaritzburg City Hall" },
                    { 4, 200, "http://example.com/images/kzn_museum.jpg", "237 Jabu Ndlovu Street, Pietermaritzburg", "KZN Museum" },
                    { 5, 2500, "http://example.com/images/pmb_oval.jpg", "Pietermaritzburg Oval, Pietermaritzburg", "Pietermaritzburg Oval" }
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventID", "Description", "EventDate", "EventName", "VenueID" },
                values: new object[,]
                {
                    { 1, "A weekend of live jazz performances by local and international artists.", new DateTime(2025, 5, 20, 18, 0, 0, 0, DateTimeKind.Unspecified), "PMB Jazz Festival", 1 },
                    { 2, "A major agricultural show with exhibitions, competitions, and entertainment.", new DateTime(2025, 6, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Royal Show", 2 },
                    { 3, "A classical music concert featuring renowned orchestras.", new DateTime(2025, 7, 10, 19, 30, 0, 0, DateTimeKind.Unspecified), "City Hall Concert", 3 },
                    { 4, "An exhibition showcasing the latest in science and technology.", new DateTime(2025, 8, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), "Science Exhibition", 4 },
                    { 5, "The highly anticipated rugby final at the Pietermaritzburg Oval.", new DateTime(2025, 9, 5, 15, 0, 0, 0, DateTimeKind.Unspecified), "Pietermaritzburg Rugby Final", 5 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "BookingID", "BookingDate", "EventID", "Status", "VenueID" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 4, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, "Confirmed", 1 },
                    { 2, new DateTime(2025, 4, 18, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, "Pending", 2 },
                    { 3, new DateTime(2025, 4, 20, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, "Confirmed", 3 },
                    { 4, new DateTime(2025, 4, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), 4, "Pending", 4 },
                    { 5, new DateTime(2025, 5, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 5, "Confirmed", 5 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "BookingID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Venues",
                keyColumn: "VenueID",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");
        }
    }
}
