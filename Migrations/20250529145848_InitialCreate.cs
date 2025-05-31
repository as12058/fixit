using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fixit.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fixit_schema");

            migrationBuilder.CreateTable(
                name: "CATEGORIES",
                schema: "fixit_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CATEGORI__3213E83FC5E0C72F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                schema: "fixit_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Cities__3213E83F2A1DADC9", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SERVICES",
                schema: "fixit_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Category_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SERVICES__3213E83F18C24640", x => x.id);
                    table.ForeignKey(
                        name: "FK__SERVICES__Catego__2B0A656D",
                        column: x => x.Category_id,
                        principalSchema: "fixit_schema",
                        principalTable: "CATEGORIES",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "fixit_schema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    last_name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    phone_number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    city_id = table.Column<int>(type: "int", nullable: true),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    user_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true, defaultValueSql: "(getdate())"),
                    GoogleId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.Id);
                    table.ForeignKey(
                        name: "FK__USERS__city_id__339FAB6E",
                        column: x => x.city_id,
                        principalSchema: "fixit_schema",
                        principalTable: "Cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                schema: "fixit_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    rating = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CUSTOMER__3213E83F23DDC27B", x => x.id);
                    table.ForeignKey(
                        name: "FK__CUSTOMERS__user___367C1819",
                        column: x => x.user_id,
                        principalSchema: "fixit_schema",
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PROFESSIONALS",
                schema: "fixit_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    hourly_rate = table.Column<int>(type: "int", nullable: true),
                    experience = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    availability = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PROFESSI__3213E83F986AA083", x => x.id);
                    table.ForeignKey(
                        name: "FK__PROFESSIO__user___395884C4",
                        column: x => x.user_id,
                        principalSchema: "fixit_schema",
                        principalTable: "USERS",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                schema: "fixit_schema",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    Professional_id = table.Column<int>(type: "int", nullable: true),
                    Service_id = table.Column<int>(type: "int", nullable: true),
                    Professional_rating = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    city_id = table.Column<int>(type: "int", nullable: true),
                    Start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    Start_time = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__3213E83F558EE572", x => x.id);
                    table.ForeignKey(
                        name: "FK__Bookings__Profes__40F9A68C",
                        column: x => x.Professional_id,
                        principalSchema: "fixit_schema",
                        principalTable: "PROFESSIONALS",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bookings__Servic__41EDCAC5",
                        column: x => x.Service_id,
                        principalSchema: "fixit_schema",
                        principalTable: "SERVICES",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bookings__city_i__42E1EEFE",
                        column: x => x.city_id,
                        principalSchema: "fixit_schema",
                        principalTable: "Cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Bookings__custom__40058253",
                        column: x => x.customer_id,
                        principalSchema: "fixit_schema",
                        principalTable: "CUSTOMERS",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Prof_cities",
                schema: "fixit_schema",
                columns: table => new
                {
                    Professional_id = table.Column<int>(type: "int", nullable: false),
                    city_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prof_cit__5828C9A5407873A4", x => new { x.Professional_id, x.city_id });
                    table.ForeignKey(
                        name: "FK__Prof_citi__Profe__45BE5BA9",
                        column: x => x.Professional_id,
                        principalSchema: "fixit_schema",
                        principalTable: "PROFESSIONALS",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Prof_citi__city___46B27FE2",
                        column: x => x.city_id,
                        principalSchema: "fixit_schema",
                        principalTable: "Cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Prof_SERVICES",
                schema: "fixit_schema",
                columns: table => new
                {
                    Professional_id = table.Column<int>(type: "int", nullable: false),
                    SERVICE_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Prof_SER__FB1ACB8A18C0FEE6", x => new { x.Professional_id, x.SERVICE_id });
                    table.ForeignKey(
                        name: "FK__Prof_SERV__Profe__498EEC8D",
                        column: x => x.Professional_id,
                        principalSchema: "fixit_schema",
                        principalTable: "PROFESSIONALS",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__Prof_SERV__SERVI__4A8310C6",
                        column: x => x.SERVICE_id,
                        principalSchema: "fixit_schema",
                        principalTable: "SERVICES",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_city_id",
                schema: "fixit_schema",
                table: "Bookings",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customer_id",
                schema: "fixit_schema",
                table: "Bookings",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Professional_id",
                schema: "fixit_schema",
                table: "Bookings",
                column: "Professional_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_Service_id",
                schema: "fixit_schema",
                table: "Bookings",
                column: "Service_id");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERS_user_id",
                schema: "fixit_schema",
                table: "CUSTOMERS",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prof_cities_city_id",
                schema: "fixit_schema",
                table: "Prof_cities",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_Prof_SERVICES_SERVICE_id",
                schema: "fixit_schema",
                table: "Prof_SERVICES",
                column: "SERVICE_id");

            migrationBuilder.CreateIndex(
                name: "IX_PROFESSIONALS_user_id",
                schema: "fixit_schema",
                table: "PROFESSIONALS",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_SERVICES_Category_id",
                schema: "fixit_schema",
                table: "SERVICES",
                column: "Category_id");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_city_id",
                schema: "fixit_schema",
                table: "USERS",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "UQ__USERS__AB6E61641FB6F851",
                schema: "fixit_schema",
                table: "USERS",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "Prof_cities",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "Prof_SERVICES",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "CUSTOMERS",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "PROFESSIONALS",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "SERVICES",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "CATEGORIES",
                schema: "fixit_schema");

            migrationBuilder.DropTable(
                name: "Cities",
                schema: "fixit_schema");
        }
    }
}
