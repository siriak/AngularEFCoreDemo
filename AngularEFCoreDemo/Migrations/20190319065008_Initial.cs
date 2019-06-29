using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace AngularEFCoreDemo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookEditions",
                columns: table => new
                {
                    BookEditionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookEditions", x => x.BookEditionId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    DeathDate = table.Column<DateTime>(nullable: false),
                    Role = table.Column<byte>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    SectionId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Building = table.Column<int>(nullable: false),
                    Room = table.Column<int>(nullable: false),
                    Cabinet = table.Column<int>(nullable: false),
                    Shelf = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.SectionId);
                });

            migrationBuilder.CreateTable(
                name: "AuthorityEntries",
                columns: table => new
                {
                    AuthorityEntryId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Order = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    BookEditionId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorityEntries", x => x.AuthorityEntryId);
                    table.ForeignKey(
                        name: "FK_AuthorityEntries_BookEditions_BookEditionId",
                        column: x => x.BookEditionId,
                        principalTable: "BookEditions",
                        principalColumn: "BookEditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorityEntries_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReaderTickets",
                columns: table => new
                {
                    ReaderTicketId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReaderId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReaderTickets", x => x.ReaderTicketId);
                    table.ForeignKey(
                        name: "FK_ReaderTickets_People_ReaderId",
                        column: x => x.ReaderId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookEditionId = table.Column<int>(nullable: false),
                    SectionId = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_BookEditions_BookEditionId",
                        column: x => x.BookEditionId,
                        principalTable: "BookEditions",
                        principalColumn: "BookEditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Books_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "SectionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookTakeEvents",
                columns: table => new
                {
                    BookTakeEventId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BookId = table.Column<int>(nullable: false),
                    TicketId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    GiveDate = table.Column<DateTime>(nullable: false),
                    ExpectedReturnDate = table.Column<DateTime>(nullable: false),
                    ActualReturnDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTakeEvents", x => x.BookTakeEventId);
                    table.ForeignKey(
                        name: "FK_BookTakeEvents_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTakeEvents_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookTakeEvents_ReaderTickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "ReaderTickets",
                        principalColumn: "ReaderTicketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorityEntries_BookEditionId",
                table: "AuthorityEntries",
                column: "BookEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorityEntries_PersonId",
                table: "AuthorityEntries",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookEditionId",
                table: "Books",
                column: "BookEditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_SectionId",
                table: "Books",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTakeEvents_BookId",
                table: "BookTakeEvents",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTakeEvents_PersonId",
                table: "BookTakeEvents",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTakeEvents_TicketId",
                table: "BookTakeEvents",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_ReaderTickets_ReaderId",
                table: "ReaderTickets",
                column: "ReaderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorityEntries");

            migrationBuilder.DropTable(
                name: "BookTakeEvents");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "ReaderTickets");

            migrationBuilder.DropTable(
                name: "BookEditions");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "People");
        }
    }
}
