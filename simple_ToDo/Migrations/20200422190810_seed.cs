using Microsoft.EntityFrameworkCore.Migrations;

namespace simple_ToDo.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "00000000-ffff-ffff-ffff-ffffffffffff", 0, "271f91c5-6632-4e0e-99b0-422313cc8db4", "joe@admin.com", true, "joe", "data", false, null, "JOE@ADMIN.COM", "JOE@ADMIN.COM", "AQAAAAEAACcQAAAAEGTeEn4tV+THok7PAUUi3eQTbJAHTJzM3m8Mnc2YZx9dzD5HasMwSWFR8JOSH5k4OA==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794577", false, "joe@admin.com" });

            migrationBuilder.InsertData(
                table: "ToDoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Not starting" },
                    { 2, "Workinonit" },
                    { 3, "FIN" }
                });

            migrationBuilder.InsertData(
                table: "TodoItem",
                columns: new[] { "Id", "ApplicationUserId", "Title", "TodoStatusId" },
                values: new object[] { 1, "00000000-ffff-ffff-ffff-ffffffffffff", "Mow the dog", 1 });

            migrationBuilder.InsertData(
                table: "TodoItem",
                columns: new[] { "Id", "ApplicationUserId", "Title", "TodoStatusId" },
                values: new object[] { 2, "00000000-ffff-ffff-ffff-ffffffffffff", "Walk the yard", 2 });

            migrationBuilder.InsertData(
                table: "TodoItem",
                columns: new[] { "Id", "ApplicationUserId", "Title", "TodoStatusId" },
                values: new object[] { 3, "00000000-ffff-ffff-ffff-ffffffffffff", "Yell at void", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodoItem",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.DeleteData(
                table: "ToDoStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToDoStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToDoStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
