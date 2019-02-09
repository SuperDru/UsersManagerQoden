using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersManager.Migrations
{
    public partial class TMigr3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_salary_rate_requests_users_manager_id",
                table: "salary_rate_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_users_users_manager_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "manager_id",
                table: "users",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "salary_rate_requests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "manager_id",
                table: "salary_rate_requests",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "fk_salary_rate_requests_users_manager_id",
                table: "salary_rate_requests",
                column: "manager_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_users_users_manager_id",
                table: "users",
                column: "manager_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_salary_rate_requests_users_manager_id",
                table: "salary_rate_requests");

            migrationBuilder.DropForeignKey(
                name: "fk_users_users_manager_id",
                table: "users");

            migrationBuilder.AlterColumn<int>(
                name: "manager_id",
                table: "users",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "salary_rate_requests",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "manager_id",
                table: "salary_rate_requests",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "fk_salary_rate_requests_users_manager_id",
                table: "salary_rate_requests",
                column: "manager_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_users_users_manager_id",
                table: "users",
                column: "manager_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
