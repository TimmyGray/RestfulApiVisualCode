using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulApiVisualCode.Migrations
{
    public partial class addmigrationfirst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Dateofevent = table.Column<string>(type: "TEXT", nullable: true),
                    Nameofasb = table.Column<int>(type: "INTEGER", nullable: false),
                    Nameofdevice = table.Column<string>(type: "TEXT", nullable: true),
                    Isserios = table.Column<string>(type: "TEXT", nullable: true),
                    Discribeevent = table.Column<string>(type: "TEXT", nullable: true),
                    Fixevent = table.Column<string>(type: "TEXT", nullable: true),
                    EventCreator = table.Column<string>(type: "TEXT", nullable: true),
                    tags = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                });

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Header = table.Column<string>(type: "TEXT", nullable: true),
                    Subheader = table.Column<string>(type: "TEXT", nullable: true),
                    Info = table.Column<string>(type: "TEXT", nullable: true),
                    PageCreator = table.Column<string>(type: "TEXT", nullable: true),
                    tags = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    ImageByte = table.Column<byte[]>(type: "BLOB", nullable: true),
                    EventId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "EventCreator", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice", "tags" },
                values: new object[] { 1, "7/1/2022", "ваще коллапс", "Admin", "все пофиксили", "серьезно", 1, "SSL", "7/1/2022,ваще коллапс,Admin,все пофиксили,серьезно,1, SSL" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "EventCreator", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice", "tags" },
                values: new object[] { 2, "7/1/2022", "все сломалось", "Admin", "это не", "не серьезно", 2, "Karrera", "7/1/2022,все сломалось,Admin,это не,не серьезно,2, Karrera" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "EventCreator", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice", "tags" },
                values: new object[] { 3, "7/1/2022", "ваще коллапс", "Admin", "все пофиксили", "не серьезно", 7, "микрофон", null });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "EventCreator", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice", "tags" },
                values: new object[] { 4, "7/1/2022", "жесть какая", "Admin", "это не", "серьезно", 5, "Dalet", "7/1/2022,жесть какая,Admin,это не,серьезн,5, Dalet" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "PageCreator", "Subheader", "tags" },
                values: new object[] { 1, "Видеопульт", "Вот тут написано как делать окна и всякие эффекты", "Admin", "Эффекты, окна", null });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "PageCreator", "Subheader", "tags" },
                values: new object[] { 2, "Видеопульт", "Вот тут сбор всяких поломок", "Admin", "Разного рода поломки", null });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "PageCreator", "Subheader", "tags" },
                values: new object[] { 3, "Камеры, CCU и OCP", "Вот тут написано о великой полезности камер", "Admin", "Камеры", null });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "PageCreator", "Subheader", "tags" },
                values: new object[] { 4, "Звуковое оборудование", "Вот здесь написано про микрофоны и их особенности", "Admin", "Микрофоны", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[] { 2, "Engeneer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "Password", "RoleId" },
                values: new object[] { 1, "Admin", "Admin", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "Password", "RoleId" },
                values: new object[] { 2, "asb6", "asb6", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "Password", "RoleId" },
                values: new object[] { 3, "asb4", "asb4", 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Images_EventId",
                table: "Images",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
