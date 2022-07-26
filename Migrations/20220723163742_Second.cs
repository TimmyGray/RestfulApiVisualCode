using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulApiVisualCode.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "EventId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
