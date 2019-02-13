using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UsersManager.Migrations
{
    public partial class Migr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hashed_credentialses");

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("a4076df9-d67b-4594-87d7-5cc89ed37962"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("a4076df9-d67b-4594-87d7-5cc89ed37962"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateTable(
                name: "hashed_credentials",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    salt = table.Column<byte[]>(nullable: true),
                    hashed_password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hashed_credentials", x => x.user_id);
                });

            migrationBuilder.InsertData(
                table: "hashed_credentials",
                columns: new[] { "user_id", "hashed_password", "salt" },
                values: new object[,]
                {
                    { 1, "s54dyTocSirH9BtCopDzS3sPLvdGsPL6pCpLkjHFhvc=", new byte[] { 218, 195, 90, 229, 243, 116, 72, 97, 18, 57, 120, 7, 17, 114, 113, 148 } },
                    { 2, "os9LACX+RLCLGp1w2Opv1j8tQMRfKBQdMRXGZT2w4Yw=", new byte[] { 86, 5, 166, 248, 250, 39, 95, 174, 165, 193, 218, 39, 28, 158, 104, 119 } },
                    { 3, "peL9j7j5WTv8gtB4yTU3TSw70zDE6t/mukXJ2+GjyVw=", new byte[] { 43, 172, 82, 183, 93, 116, 48, 95, 203, 206, 60, 74, 107, 105, 42, 172 } }
                });

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: new Guid("26a709ce-ce27-43ba-a7d4-6e05ca5368f8"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: new Guid("19b09a1a-3fcf-45ac-88b0-c19b121c52de"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: new Guid("f95bb5bc-1c62-448a-a7ba-2daf05cde42e"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hashed_credentials");

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateTable(
                name: "hashed_credentialses",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    hashed_password = table.Column<string>(nullable: true),
                    salt = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hashed_credentialses", x => x.user_id);
                });

            migrationBuilder.InsertData(
                table: "hashed_credentialses",
                columns: new[] { "user_id", "hashed_password", "salt" },
                values: new object[,]
                {
                    { 1, "+Z/JAldyeEVF8u5sDxO5oLsxLaPVbLSrB6LCoLGiuAc=", new byte[] { 109, 255, 210, 148, 8, 32, 137, 199, 100, 47, 233, 94, 120, 223, 161, 214 } },
                    { 2, "STFECPVK0EJJKzdRGMgNxkIAhhXUQ0zt05zGevRBWVA=", new byte[] { 153, 166, 14, 47, 198, 44, 15, 97, 252, 91, 114, 179, 243, 11, 5, 242 } },
                    { 3, "UN15037KAkZZu3A02RK0AlxspiOpgv8TS6KoNUJ8pCw=", new byte[] { 85, 28, 227, 23, 172, 74, 227, 84, 177, 136, 63, 227, 218, 247, 25, 9 } }
                });

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("a4076df9-d67b-4594-87d7-5cc89ed37962"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("a4076df9-d67b-4594-87d7-5cc89ed37962"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: new Guid("dd472a94-fa67-4f12-a650-fe3de66a7ee2"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: new Guid("afd6a34b-1acf-4ed1-abb3-d4afaf49f9a7"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: new Guid("143f1b99-9fe5-4176-abdd-581ace731308"));
        }
    }
}
