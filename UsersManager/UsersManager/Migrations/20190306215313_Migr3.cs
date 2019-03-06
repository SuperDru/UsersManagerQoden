using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UsersManager.Migrations
{
    public partial class Migr3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_salary_rates_users_user_id",
                table: "salary_rates");

            migrationBuilder.DropIndex(
                name: "ix_salary_rates_user_id",
                table: "salary_rates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_hashed_credentials",
                table: "hashed_credentials");

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("744c774f-25eb-4696-8aba-8ec2ca362216"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "hashed_credentials",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "pk_hashed_credentials",
                table: "hashed_credentials",
                column: "user_id");

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "M/kCV1r3olAPKyV1kFWvwm+q7EHhlNy+3Hb7h+GZt7Q=", new byte[] { 145, 125, 23, 211, 205, 193, 142, 217, 108, 28, 149, 125, 235, 182, 254, 1 } });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "tbF15tbhBLeSBpteGHsopTexwic14Vn9pwYu7NGwM1w=", new byte[] { 45, 184, 34, 192, 33, 29, 122, 75, 238, 22, 26, 177, 2, 211, 107, 179 } });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 3,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "ojeAkISmrgunQqIjCuIWCkV4XXY6kIMzjfUbzccaziA=", new byte[] { 135, 56, 224, 16, 228, 183, 175, 86, 31, 232, 69, 224, 99, 215, 19, 75 } });

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("284df7a9-a327-42c1-8569-c435d5dc51aa"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("284df7a9-a327-42c1-8569-c435d5dc51aa"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: new Guid("bd416a6f-1ca9-4bc7-8481-57095f4a1050"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: new Guid("01eb8bc6-ce45-4c46-ac06-b0c691cfc460"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: new Guid("9d1e9804-505d-4705-a156-5144e09c1e99"));

            migrationBuilder.AddForeignKey(
                name: "fk_hashed_credentials_users_user_id",
                table: "hashed_credentials",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_hashed_credentials_users_user_id",
                table: "hashed_credentials");

            migrationBuilder.DropPrimaryKey(
                name: "pk_hashed_credentials",
                table: "hashed_credentials");

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("284df7a9-a327-42c1-8569-c435d5dc51aa"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("284df7a9-a327-42c1-8569-c435d5dc51aa"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                table: "hashed_credentials",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_hashed_credentials",
                table: "hashed_credentials",
                column: "user_id");

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "s54dyTocSirH9BtCopDzS3sPLvdGsPL6pCpLkjHFhvc=", new byte[] { 218, 195, 90, 229, 243, 116, 72, 97, 18, 57, 120, 7, 17, 114, 113, 148 } });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "os9LACX+RLCLGp1w2Opv1j8tQMRfKBQdMRXGZT2w4Yw=", new byte[] { 86, 5, 166, 248, 250, 39, 95, 174, 165, 193, 218, 39, 28, 158, 104, 119 } });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 3,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "peL9j7j5WTv8gtB4yTU3TSw70zDE6t/mukXJ2+GjyVw=", new byte[] { 43, 172, 82, 183, 93, 116, 48, 95, 203, 206, 60, 74, 107, 105, 42, 172 } });

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

            migrationBuilder.CreateIndex(
                name: "ix_salary_rates_user_id",
                table: "salary_rates",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_salary_rates_users_user_id",
                table: "salary_rates",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
