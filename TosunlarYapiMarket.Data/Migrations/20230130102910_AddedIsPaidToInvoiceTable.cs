using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TosunlarYapiMarket.Data.Migrations
{
    public partial class AddedIsPaidToInvoiceTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Invoice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "2f0ca96f-c729-4fb6-a3d8-5a625c282665");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f4f7b4d-321a-4dab-a0a9-5242490b9edd", "AQAAAAEAACcQAAAAEGm7pQlxUEO8ACTAcQr+sV7V04Rq5zoSrdixg+tECA6X1D5gyIFp7iTv9W5q9BBn+Q==", "b9d5a1d5-1bbb-4a19-aadd-471416da98ef" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Invoice");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a12c2d04-a9a7-42e8-b828-9e1003179a86");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ee8fd908-ae3b-4349-b510-93b4d7bb4868", "AQAAAAEAACcQAAAAEGW3wKPmGax3XAl1mossfx65v1IXTsFW1OqmRVBwaDNU7zAmN4H43OdyV9UNM5iOFw==", "e9da579c-0ae0-4293-88b1-f8b9e15b3af1" });
        }
    }
}
