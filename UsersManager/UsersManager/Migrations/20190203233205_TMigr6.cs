using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersManager.Migrations
{
    public partial class TMigr6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { "81305a7e-d967-41c7-8b57-bb87df2452ae", new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { "81305a7e-d967-41c7-8b57-bb87df2452ae", new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "salary_rates",
                columns: new[] { "updated_at", "user_id", "rate" },
                values: new object[,]
                {
                    { new DateTime(2014, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1000 },
                    { new DateTime(2016, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1200 },
                    { new DateTime(2015, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1500 },
                    { new DateTime(2015, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1800 },
                    { new DateTime(2011, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 2500 }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: "7b575d32-c324-4d06-87b4-5b2438bca565");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: "4fb52256-dd39-4045-82cf-df93d1dd7647");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: "bb6ec5f3-3899-427d-be1e-94de6c737c21");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { "81305a7e-d967-41c7-8b57-bb87df2452ae", new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rate_requests",
                keyColumns: new[] { "guid", "updated_at" },
                keyValues: new object[] { "81305a7e-d967-41c7-8b57-bb87df2452ae", new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.DeleteData(
                table: "salary_rates",
                keyColumns: new[] { "updated_at", "user_id" },
                keyValues: new object[] { new DateTime(2011, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 });

            migrationBuilder.DeleteData(
                table: "salary_rates",
                keyColumns: new[] { "updated_at", "user_id" },
                keyValues: new object[] { new DateTime(2014, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.DeleteData(
                table: "salary_rates",
                keyColumns: new[] { "updated_at", "user_id" },
                keyValues: new object[] { new DateTime(2015, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.DeleteData(
                table: "salary_rates",
                keyColumns: new[] { "updated_at", "user_id" },
                keyValues: new object[] { new DateTime(2015, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 });

            migrationBuilder.DeleteData(
                table: "salary_rates",
                keyColumns: new[] { "updated_at", "user_id" },
                keyValues: new object[] { new DateTime(2016, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "guid",
                value: "682b00cb-f5cd-4f02-907b-89c4f187cc32");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 2,
                column: "guid",
                value: "596a7661-cf39-43d5-ba32-685481bf2813");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 3,
                column: "guid",
                value: "0e36684b-88bb-4ea3-9d93-1fb41827aab1");
        }
    }
}
