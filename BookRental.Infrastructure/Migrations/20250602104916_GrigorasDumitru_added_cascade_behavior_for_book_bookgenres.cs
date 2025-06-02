using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookRental.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GrigorasDumitru_added_cascade_behavior_for_book_bookgenres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Books_BooksId",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Genres_GenresId",
                table: "BookGenres");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "BookGenres",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "BooksId",
                table: "BookGenres",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenres_GenresId",
                table: "BookGenres",
                newName: "IX_BookGenres_GenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Books_BookId",
                table: "BookGenres",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Genres_GenreId",
                table: "BookGenres",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Books_BookId",
                table: "BookGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_BookGenres_Genres_GenreId",
                table: "BookGenres");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "BookGenres",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookGenres",
                newName: "BooksId");

            migrationBuilder.RenameIndex(
                name: "IX_BookGenres_GenreId",
                table: "BookGenres",
                newName: "IX_BookGenres_GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Books_BooksId",
                table: "BookGenres",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookGenres_Genres_GenresId",
                table: "BookGenres",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
