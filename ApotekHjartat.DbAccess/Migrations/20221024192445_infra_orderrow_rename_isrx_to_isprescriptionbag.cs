using Microsoft.EntityFrameworkCore.Migrations;

namespace ApotekHjartat.DbAccess.Migrations
{
    public partial class infra_orderrow_rename_isrx_to_isprescriptionbag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRx",
                table: "CustomerOrderRow");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrescriptionBag",
                table: "CustomerOrderRow",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrescriptionBag",
                table: "CustomerOrderRow");

            migrationBuilder.AddColumn<bool>(
                name: "IsRx",
                table: "CustomerOrderRow",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
