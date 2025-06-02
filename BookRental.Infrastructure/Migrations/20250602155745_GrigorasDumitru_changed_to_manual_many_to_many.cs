using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GrigorasDumitru_changed_to_manual_many_to_many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "BookGenres",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "CreatedAt",
                table: "BookGenres",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "BookGenres",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DeletedAt",
                table: "BookGenres",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "BookGenres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "BookGenres",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "UpdatedAt",
                table: "BookGenres",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "BookGenres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres");

            migrationBuilder.DropIndex(
                name: "IX_BookGenres_BookId",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "BookGenres");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "BookGenres");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookGenres",
                table: "BookGenres",
                columns: new[] { "BookId", "GenreId" });
        }
    }
}
