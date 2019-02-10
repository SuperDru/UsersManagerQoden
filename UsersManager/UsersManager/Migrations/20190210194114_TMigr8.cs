using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UsersManager.Migrations
{
    public partial class TMigr8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("9f034ae5-f282-46d5-a359-78b15b82397d"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("9f034ae5-f282-46d5-a359-78b15b82397d"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DropColumn(
                name: "password",
                table: "users");

            migrationBuilder.CreateTable(
                name: "hashed_credentialses",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    salt = table.Column<byte[]>(nullable: true),
                    hashed_password = table.Column<string>(nullable: true)
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
                    { 1, "1xiJ6OfHF7Ao1VRB0djBPuYj4VABHTmKs0OG2dLK8us=", new byte[] { 118, 8, 206, 53, 64, 174, 193, 160, 13, 211, 188, 22, 201, 247, 72, 2 } },
                    { 2, "cB968Ix4ZSsDDdOi3BuXbSpCDzNeDYqOBVCliSq0HGA=", new byte[] { 242, 90, 59, 194, 185, 182, 93, 36, 69, 68, 199, 120, 245, 100, 158, 46 } },
                    { 3, "fPze+53KclTkKk9GLbXRbtmhKzLC66p8V2eamYTapK8=", new byte[] { 17, 50, 143, 198, 249, 171, 178, 238, 148, 37, 236, 171, 229, 164, 96, 121 } }
                });

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("3738845f-a969-400a-a777-e11d47a32707"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("3738845f-a969-400a-a777-e11d47a32707"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: new Guid("4977f511-3cdc-4073-86dc-d830d14e64d3"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: new Guid("f638ba29-2fc0-4ce4-b264-af5e46d9bfc2"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: new Guid("4524b820-ea62-4097-ae37-6581e5cc4f99"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hashed_credentialses");

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("3738845f-a969-400a-a777-e11d47a32707"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("3738845f-a969-400a-a777-e11d47a32707"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AddColumn<string>(
                name: "password",
                table: "users",
                nullable: true);

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("9f034ae5-f282-46d5-a359-78b15b82397d"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("9f034ae5-f282-46d5-a359-78b15b82397d"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "guid", "password" },
                values: new object[] { new Guid("a2bb9c11-b5ac-4c1d-9c04-8f025b662fbd"), "1" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                columns: new[] { "guid", "password" },
                values: new object[] { new Guid("2e6ccbd4-86d7-499a-9d37-b00775453dc2"), "12" });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                columns: new[] { "guid", "password" },
                values: new object[] { new Guid("8e74485c-ab9b-4843-8103-1ce94d71196f"), "123" });
        }
    }
}
