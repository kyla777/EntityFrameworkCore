using Microsoft.EntityFrameworkCore.Migrations;

namespace QuickReach.ECommerce.Infra.Data.Migrations
{
    public partial class AddManufacturerandProductManufacturer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturerProduct_Manufacturer_ManufacturerID",
                table: "ManufacturerProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ManufacturerProduct_Product_ProductID",
                table: "ManufacturerProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ManufacturerProduct",
                table: "ManufacturerProduct");

            migrationBuilder.RenameTable(
                name: "ManufacturerProduct",
                newName: "ProductManufacturer");

            migrationBuilder.RenameIndex(
                name: "IX_ManufacturerProduct_ProductID",
                table: "ProductManufacturer",
                newName: "IX_ProductManufacturer_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductManufacturer",
                table: "ProductManufacturer",
                columns: new[] { "ManufacturerID", "ProductID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManufacturer_Manufacturer_ManufacturerID",
                table: "ProductManufacturer",
                column: "ManufacturerID",
                principalTable: "Manufacturer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductManufacturer_Product_ProductID",
                table: "ProductManufacturer",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductManufacturer_Manufacturer_ManufacturerID",
                table: "ProductManufacturer");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductManufacturer_Product_ProductID",
                table: "ProductManufacturer");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductManufacturer",
                table: "ProductManufacturer");

            migrationBuilder.RenameTable(
                name: "ProductManufacturer",
                newName: "ManufacturerProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ProductManufacturer_ProductID",
                table: "ManufacturerProduct",
                newName: "IX_ManufacturerProduct_ProductID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ManufacturerProduct",
                table: "ManufacturerProduct",
                columns: new[] { "ManufacturerID", "ProductID" });

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturerProduct_Manufacturer_ManufacturerID",
                table: "ManufacturerProduct",
                column: "ManufacturerID",
                principalTable: "Manufacturer",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ManufacturerProduct_Product_ProductID",
                table: "ManufacturerProduct",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
