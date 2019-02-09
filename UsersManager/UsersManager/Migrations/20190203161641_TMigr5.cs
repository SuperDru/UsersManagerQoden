using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersManager.Migrations
{
    public partial class TMigr5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "guid", "password" },
                values: new object[] { "682b00cb-f5cd-4f02-907b-89c4f187cc32", "1" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "guid", "password" },
                values: new object[] { "596a7661-cf39-43d5-ba32-685481bf2813", "12" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "guid", "password" },
                values: new object[] { "0e36684b-88bb-4ea3-9d93-1fb41827aab1", "123" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "guid", "password" },
                values: new object[] { "a1dcacae-d4c5-4648-970e-4b21d5941cd4", null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "guid", "password" },
                values: new object[] { "19cc755a-e32f-436e-9153-7d9c337a207e", null });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "guid", "password" },
                values: new object[] { "5787f3de-21ad-4c1b-b408-f97ae2bebaf0", null });
        }
    }
}
