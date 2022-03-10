using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulApiVisualCode.Migrations
{
    public partial class Initialize : Migration
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
                    Fixevent = table.Column<string>(type: "TEXT", nullable: true)
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
                    Info = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice" },
                values: new object[] { 1, "3/11/2022", "ваще коллапс", "все пофиксили", "серьезно", 1, "SSL" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice" },
                values: new object[] { 2, "3/11/2022", "все сломалось", "это не", "не серьезно", 2, "Karrera" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice" },
                values: new object[] { 3, "3/11/2022", "ваще коллапс", "все пофиксили", "не серьезно", 7, "микрофон" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Dateofevent", "Discribeevent", "Fixevent", "Isserios", "Nameofasb", "Nameofdevice" },
                values: new object[] { 4, "3/11/2022", "жесть какая", "это не", "серьезно", 5, "Karrera" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "Subheader" },
                values: new object[] { 1, "Видеопульт", "Вот тут написано как делать окна и всякие эффекты", "Эффекты, окна" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "Subheader" },
                values: new object[] { 2, "Видеопульт", "Вот тут сбор всяких поломок", "Разного рода поломки" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "Subheader" },
                values: new object[] { 3, "Камеры, CCU и OCP", "Вот тут написано о великой полезности камер", "Камеры" });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "Header", "Info", "Subheader" },
                values: new object[] { 4, "Звуковое оборудование", "Вот здесь написано про микрофоны и их особенности", "Микрофоны" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Login", "Password" },
                values: new object[] { 1, "Timmy", "1234" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
