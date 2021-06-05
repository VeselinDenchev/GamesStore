using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Change_AgeRating_and_DiscountPercentage_type_to_sbyte_and_fix_DiscountPercentage_name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercantage",
                table: "DiscountCodes");

            migrationBuilder.AlterColumn<short>(
                name: "AgeRating",
                table: "Games",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddColumn<short>(
                name: "DiscountPercentage",
                table: "DiscountCodes",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "DiscountCodes");

            migrationBuilder.AlterColumn<byte>(
                name: "AgeRating",
                table: "Games",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddColumn<byte>(
                name: "DiscountPercantage",
                table: "DiscountCodes",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
