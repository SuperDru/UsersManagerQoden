using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace UsersManager.Migrations
{
    public partial class Migr1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_departments", x => x.id);
                });

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

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    guid = table.Column<Guid>(nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    patronymic = table.Column<string>(nullable: true),
                    nick_name = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    phone_number = table.Column<long>(nullable: false),
                    invited_at = table.Column<DateTime>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    department_id = table.Column<int>(nullable: false),
                    manager_id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_departments_department_id",
                        column: x => x.department_id,
                        principalTable: "departments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_users_users_manager_id",
                        column: x => x.manager_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "salary_rate_requests",
                columns: table => new
                {
                    guid = table.Column<Guid>(nullable: false),
                    updated_at = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    rate = table.Column<int>(nullable: false),
                    reasons = table.Column<string>(nullable: true),
                    private_explanation = table.Column<string>(nullable: true),
                    explanation = table.Column<string>(nullable: true),
                    manager_id = table.Column<int>(nullable: true),
                    status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary_rate_requests", x => new { x.guid, x.updated_at });
                    table.ForeignKey(
                        name: "fk_salary_rate_requests_users_manager_id",
                        column: x => x.manager_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_salary_rate_requests_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "salary_rates",
                columns: table => new
                {
                    updated_at = table.Column<DateTime>(nullable: false),
                    user_id = table.Column<int>(nullable: false),
                    rate = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_salary_rates", x => new { x.updated_at, x.user_id });
                    table.ForeignKey(
                        name: "fk_salary_rates_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                columns: table => new
                {
                    user_id = table.Column<int>(nullable: false),
                    role_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.role_id, x.user_id });
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "developer" },
                    { 2, "manager" }
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
                columns: new[] { "id", "department_id", "description", "email", "first_name", "guid", "invited_at", "last_name", "manager_id", "nick_name", "patronymic", "phone_number" },
                values: new object[,]
                {
                    { 1, 1, "American", "jfoster@gmail.com", "Jhon", new Guid("dd472a94-fa67-4f12-a650-fe3de66a7ee2"), new DateTime(2014, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Foster", null, "JFoster", null, 89129541254L },
                    { 2, 2, "Russian", "ashishkin@mail.ru", "Alexander", new Guid("afd6a34b-1acf-4ed1-abb3-d4afaf49f9a7"), new DateTime(2015, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shishkin", null, "AShishkin", "Dmitrievich", 83149545254L },
                    { 3, 2, "Russian", "ashurikov@mail.ru", "Andrey", new Guid("143f1b99-9fe5-4176-abdd-581ace731308"), new DateTime(2011, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shurikov", null, "AShurikov", "Vasilievich", 83149565253L }
                });

            migrationBuilder.InsertData(
                table: "salary_rate_requests",
                columns: new[] { "guid", "updated_at", "explanation", "manager_id", "private_explanation", "rate", "reasons", "status", "user_id" },
                values: new object[,]
                {
                    { new Guid("a4076df9-d67b-4594-87d7-5cc89ed37962"), new DateTime(2017, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "You haven't enough experience.", 2, "He's way too dumb.", 1500, "Just so", 3, 1 },
                    { new Guid("a4076df9-d67b-4594-87d7-5cc89ed37962"), new DateTime(2017, 5, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 2, null, 1500, "Just so", 1, 1 }
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

            migrationBuilder.InsertData(
                table: "user_roles",
                columns: new[] { "role_id", "user_id" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 2, 2 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_salary_rate_requests_manager_id",
                table: "salary_rate_requests",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "ix_salary_rate_requests_user_id",
                table: "salary_rate_requests",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_salary_rates_user_id",
                table: "salary_rates",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_roles_user_id",
                table: "user_roles",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_department_id",
                table: "users",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_manager_id",
                table: "users",
                column: "manager_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_nick_name",
                table: "users",
                column: "nick_name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hashed_credentialses");

            migrationBuilder.DropTable(
                name: "salary_rate_requests");

            migrationBuilder.DropTable(
                name: "salary_rates");

            migrationBuilder.DropTable(
                name: "user_roles");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
