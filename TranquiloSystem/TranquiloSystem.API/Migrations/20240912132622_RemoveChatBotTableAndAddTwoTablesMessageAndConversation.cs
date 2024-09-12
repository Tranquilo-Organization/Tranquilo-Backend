using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranquiloSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveChatBotTableAndAddTwoTablesMessageAndConversation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Levels_LevelId",
                table: "Routines");

            migrationBuilder.DropTable(
                name: "ChatBotInteractions");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Routines",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "BotConversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotConversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotConversations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BotConversationId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFromUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_BotConversations_BotConversationId",
                        column: x => x.BotConversationId,
                        principalTable: "BotConversations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotConversations_UserId",
                table: "BotConversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_BotConversationId",
                table: "Messages",
                column: "BotConversationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Levels_LevelId",
                table: "Routines",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routines_Levels_LevelId",
                table: "Routines");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "BotConversations");

            migrationBuilder.AlterColumn<int>(
                name: "LevelId",
                table: "Routines",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ChatBotInteractions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BotMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InteractionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserMessage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatBotInteractions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatBotInteractions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotInteractions_UserId",
                table: "ChatBotInteractions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routines_Levels_LevelId",
                table: "Routines",
                column: "LevelId",
                principalTable: "Levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
