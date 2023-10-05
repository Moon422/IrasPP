using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IrasPPBackend.Migrations
{
    /// <inheritdoc />
    public partial class authmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AuthId",
                table: "Users_T",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Auth_T",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HashedPassword = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_T", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_T_AuthId",
                table: "Users_T",
                column: "AuthId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_T_Auth_T_AuthId",
                table: "Users_T",
                column: "AuthId",
                principalTable: "Auth_T",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_T_Auth_T_AuthId",
                table: "Users_T");

            migrationBuilder.DropTable(
                name: "Auth_T");

            migrationBuilder.DropIndex(
                name: "IX_Users_T_AuthId",
                table: "Users_T");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Users_T");
        }
    }
}
