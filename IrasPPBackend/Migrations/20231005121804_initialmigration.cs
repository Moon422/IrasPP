using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrasPPBackend.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Schools_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools_T", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Departments_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_T_Schools_T_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Programs_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ShortCode = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Programs_T_Departments_T_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Courses_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CourseCode = table.Column<int>(type: "int", nullable: false),
                    ProgramId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_T_Programs_T_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DateOfBirth = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HouseAddress = table.Column<string>(type: "varchar(512)", maxLength: 512, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
                    FacultyRoles = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Faculty_IsActive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    SchoolId = table.Column<long>(type: "bigint", nullable: true),
                    ProgramId = table.Column<long>(type: "bigint", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    IsCurrent = table.Column<bool>(type: "tinyint(1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_T_Departments_T_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_T_Programs_T_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "Programs_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_T_Schools_T_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CourseCoordinators_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<long>(type: "bigint", nullable: false),
                    FacultyId = table.Column<long>(type: "bigint", nullable: false),
                    IsCurrent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CourseCoordinatorTill = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseCoordinators_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseCoordinators_T_Courses_T_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseCoordinators_T_Users_T_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Users_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DeanOfSchools_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SchoolId = table.Column<long>(type: "bigint", nullable: false),
                    FacultyId = table.Column<long>(type: "bigint", nullable: false),
                    IsCurrent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    DeanTill = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeanOfSchools_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeanOfSchools_T_Schools_T_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeanOfSchools_T_Users_T_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Users_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "HeadOfDepartments_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    FacultyId = table.Column<long>(type: "bigint", nullable: false),
                    IsCurrent = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HeadTill = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeadOfDepartments_T", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartments_T_Departments_T_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeadOfDepartments_T_Users_T_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Users_T",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCoordinators_T_CourseId",
                table: "CourseCoordinators_T",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseCoordinators_T_FacultyId",
                table: "CourseCoordinators_T",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_T_ProgramId",
                table: "Courses_T",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_DeanOfSchools_T_FacultyId",
                table: "DeanOfSchools_T",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_DeanOfSchools_T_SchoolId",
                table: "DeanOfSchools_T",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_T_SchoolId",
                table: "Departments_T",
                column: "SchoolId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartments_T_DepartmentId",
                table: "HeadOfDepartments_T",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_HeadOfDepartments_T_FacultyId",
                table: "HeadOfDepartments_T",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Programs_T_DepartmentId",
                table: "Programs_T",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_T_DepartmentId",
                table: "Users_T",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_T_ProgramId",
                table: "Users_T",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_T_SchoolId",
                table: "Users_T",
                column: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseCoordinators_T");

            migrationBuilder.DropTable(
                name: "DeanOfSchools_T");

            migrationBuilder.DropTable(
                name: "HeadOfDepartments_T");

            migrationBuilder.DropTable(
                name: "Courses_T");

            migrationBuilder.DropTable(
                name: "Users_T");

            migrationBuilder.DropTable(
                name: "Programs_T");

            migrationBuilder.DropTable(
                name: "Departments_T");

            migrationBuilder.DropTable(
                name: "Schools_T");
        }
    }
}
