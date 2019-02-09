using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UsersManager.Migrations
{
    public partial class TMigr2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_salary_rates",
                table: "salary_rates");

            migrationBuilder.DropPrimaryKey(
                name: "pk_salary_rate_requests",
                table: "salary_rate_requests");

            migrationBuilder.DropColumn(
                name: "id",
                table: "salary_rates");

            migrationBuilder.DropColumn(
                name: "id",
                table: "salary_rate_requests");

            migrationBuilder.AlterColumn<string>(
                name: "guid",
                table: "salary_rate_requests",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "salary_rate_requests",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_salary_rates",
                table: "salary_rates",
                columns: new[] { "updated_at", "user_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_salary_rate_requests",
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at" });

            migrationBuilder.CreateIndex(
                name: "ix_salary_rate_requests_user_id",
                table: "salary_rate_requests",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_salary_rate_requests_users_user_id",
                table: "salary_rate_requests",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_salary_rate_requests_users_user_id",
                table: "salary_rate_requests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salary_rates",
                table: "salary_rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_salary_rate_requests",
                table: "salary_rate_requests");

            migrationBuilder.DropIndex(
                name: "ix_salary_rate_requests_user_id",
                table: "salary_rate_requests");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "salary_rate_requests");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "salary_rates",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AlterColumn<string>(
                name: "guid",
                table: "salary_rate_requests",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "salary_rate_requests",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_salary_rates",
                table: "salary_rates",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_salary_rate_requests",
                table: "salary_rate_requests",
                column: "id");
        }
    }
}
