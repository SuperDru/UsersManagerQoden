using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersManager.Migrations
{
    public partial class Migr4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("284df7a9-a327-42c1-8569-c435d5dc51aa"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("284df7a9-a327-42c1-8569-c435d5dc51aa"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 1,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "2gkpeAEnA5+eYW0CvUGxg0V0nYksIuJ9oVIoqrF5C+M=", new byte[] { 78, 1, 71, 121, 68, 120, 148, 38, 34, 212, 59, 18, 23, 23, 56, 125 } });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 2,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "F1AUSYCzsR/gZBjbcSanqiPL4lnwzw4LDCCuGCM/sBQ=", new byte[] { 58, 48, 129, 69, 217, 245, 57, 189, 165, 49, 183, 180, 163, 148, 228, 162 } });

            migrationBuilder.UpdateData(
                table: "hashed_credentials",
                keyColumn: "user_id",
                keyValue: 3,
                columns: new[] { "hashed_password", "salt" },
                values: new object[] { "S8CQmd/+UkZ02TYCKZFbaJqCwLIRVTsBtI15qqynbK4=", new byte[] { 0, 202, 165, 185, 81, 112, 32, 84, 140, 167, 21, 42, 116, 21, 34, 243 } });

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("ca11ce25-5bff-48e4-a7b3-b76952074b82"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("ca11ce25-5bff-48e4-a7b3-b76952074b82"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: new Guid("2fa6e58a-483b-47ea-9550-acec85fc6595"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: new Guid("b6240f6e-e0f6-4eb0-8d0e-5e05f0137945"));

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: new Guid("832a48fe-8bc6-49c7-bb40-7bdb57451ed6"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_salary_rates_users_user_id",
                table: "salary_rates");

            migrationBuilder.DropIndex(
                name: "ix_salary_rates_user_id",
                table: "salary_rates");

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("ca11ce25-5bff-48e4-a7b3-b76952074b82"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { new Guid("ca11ce25-5bff-48e4-a7b3-b76952074b82"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

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
        }
    }
}
