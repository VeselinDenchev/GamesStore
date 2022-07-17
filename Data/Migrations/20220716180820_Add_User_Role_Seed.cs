using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Add_User_Role_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "28829b59-261f-43c0-9ee6-e35b3933d11e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "52f80c2d-a311-4ba3-994b-e111aa010801", "9e75c950-75a7-4826-b0f9-ee545ac6dc74", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "84e829d0-76cd-4e9d-b9ef-0b45ef183839", "27eb429b-09c0-4ca8-a0f4-7e04f0d49545", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "52f80c2d-a311-4ba3-994b-e111aa010801");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "84e829d0-76cd-4e9d-b9ef-0b45ef183839");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "28829b59-261f-43c0-9ee6-e35b3933d11e", "6a64c3d2-9e4d-4e7c-bdef-26a1838161ae", "Admin", "ADMIN" });
        }
    }
}
