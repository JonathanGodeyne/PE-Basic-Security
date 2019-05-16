using Microsoft.EntityFrameworkCore.Migrations;

namespace Hybrid_Crypt_IRC.Migrations.ChatCrypt
{
    public partial class ChatCryptInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GroupChat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MessageText = table.Column<string>(nullable: true),
                    SenderId = table.Column<string>(nullable: true),
                    ChatGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_GroupChat_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "GroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(nullable: true),
                    PublicKey = table.Column<string>(nullable: true),
                    ConnectionId = table.Column<string>(nullable: true),
                    ChatGroupId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInfo_GroupChat_ChatGroupId",
                        column: x => x.ChatGroupId,
                        principalTable: "GroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatGroupId",
                table: "Messages",
                column: "ChatGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInfo_ChatGroupId",
                table: "UserInfo",
                column: "ChatGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "GroupChat");
        }
    }
}
