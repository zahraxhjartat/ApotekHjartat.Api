using Microsoft.EntityFrameworkCore.Migrations;

namespace ApotekHjartat.DbAccess.Migrations
{
    public partial class infra_add_price_properties_to_orderrow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VAT",
                table: "Product",
                newName: "Vat");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceExclVat",
                table: "CustomerOrderRow",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Vat",
                table: "CustomerOrderRow",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceExclVat",
                table: "CustomerOrderRow");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "CustomerOrderRow");

            migrationBuilder.RenameColumn(
                name: "Vat",
                table: "Product",
                newName: "VAT");
        }
    }
}
