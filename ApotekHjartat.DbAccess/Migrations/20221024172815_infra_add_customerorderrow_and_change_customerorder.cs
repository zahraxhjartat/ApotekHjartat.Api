using Microsoft.EntityFrameworkCore.Migrations;

namespace ApotekHjartat.DbAccess.Migrations
{
    public partial class infra_add_customerorderrow_and_change_customerorder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRxOrder",
                table: "CustomerOrder");

            migrationBuilder.AddColumn<string>(
                name: "CustomerAddress",
                table: "CustomerOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerEmailAddress",
                table: "CustomerOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerFirstName",
                table: "CustomerOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomerSurname",
                table: "CustomerOrder",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "CustomerOrder",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerOrderRow",
                columns: table => new
                {
                    OrderRowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderedAmount = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(nullable: true),
                    CustomerOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderRow", x => x.OrderRowId);
                    table.ForeignKey(
                        name: "FK_CustomerOrderRow_CustomerOrder_CustomerOrderId",
                        column: x => x.CustomerOrderId,
                        principalTable: "CustomerOrder",
                        principalColumn: "CustomerOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderRow_CustomerOrderId",
                table: "CustomerOrderRow",
                column: "CustomerOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerOrderRow");

            migrationBuilder.DropColumn(
                name: "CustomerAddress",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "CustomerEmailAddress",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "CustomerFirstName",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "CustomerSurname",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "CustomerOrder");

            migrationBuilder.AddColumn<bool>(
                name: "IsRxOrder",
                table: "CustomerOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
