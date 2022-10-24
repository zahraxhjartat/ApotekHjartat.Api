using Microsoft.EntityFrameworkCore.Migrations;

namespace ApotekHjartat.Order.DbAccess.Migrations
{
    public partial class initial_create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerOrder",
                columns: table => new
                {
                    CustomerOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNumber = table.Column<string>(maxLength: 100, nullable: true),
                    IsRxOrder = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrder", x => x.CustomerOrderId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrder");
        }
    }
}
