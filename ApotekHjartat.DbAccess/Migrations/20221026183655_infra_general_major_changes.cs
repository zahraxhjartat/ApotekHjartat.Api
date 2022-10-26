using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApotekHjartat.DbAccess.Migrations
{
    public partial class infra_general_major_changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrescriptionBag",
                table: "CustomerOrderRow");

            migrationBuilder.AddColumn<string>(
                name: "Ean",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalProductId",
                table: "Product",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CustomerOrderRow",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderRowType",
                table: "CustomerOrderRow",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "OrderStatus",
                table: "CustomerOrder",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "CustomerOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TrackingNumber",
                table: "CustomerOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ean",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ExternalProductId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "OrderRowType",
                table: "CustomerOrderRow");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "CustomerOrder");

            migrationBuilder.DropColumn(
                name: "TrackingNumber",
                table: "CustomerOrder");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "CustomerOrderRow",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsPrescriptionBag",
                table: "CustomerOrderRow",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "OrderStatus",
                table: "CustomerOrder",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
