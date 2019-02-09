using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersManager.Migrations
{
    public partial class TMigr4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_departments_department_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "developer" },
                    { 2, "manager" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "admin" },
                    { 2, "manager" },
                    { 3, "user" }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "department_id", "description", "email", "first_name", "guid", "invited_at", "last_name", "manager_id", "nick_name", "password", "patronymic", "phone_number" },
                values: new object[,]
                {
                    { 1, 1, "American", "jfoster@gmail.com", "Jhon", "a1dcacae-d4c5-4648-970e-4b21d5941cd4", new DateTime(2014, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foster", null, "JFoster", null, null, 89129541254L },
                    { 2, 2, "Russian", "ashishkin@mail.ru", "Alexander", "19cc755a-e32f-436e-9153-7d9c337a207e", new DateTime(2015, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shishkin", null, "AShishkin", null, "Dmitrievich", 83149545254L },
                    { 3, 2, "Russian", "ashurikov@mail.ru", "Andrey", "5787f3de-21ad-4c1b-b408-f97ae2bebaf0", new DateTime(2011, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shurikov", null, "AShurikov", null, "Vasilievich", 83149565253L }
                });

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 2, 2 },
                    { 1, 3 }
                });

            migrationBuilder.AddForeignKey(
                name: "fk_users_departments_department_id",
                table: "users",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_departments_department_id",
                table: "users");

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "user_roles",
                keyColumns: new[] { "role_id", "user_id" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "departments",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "departments",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "department_id",
                table: "users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "fk_users_departments_department_id",
                table: "users",
                column: "department_id",
                principalTable: "departments",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
