using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class MigratonName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Filterr",
                columns: table => new
                {
                    Id_categories = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_availability = table.Column<bool>(type: "bit", nullable: false),
                    release_form = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    availability_of_discounts = table.Column<bool>(type: "bit", nullable: false),
                    discounts = table.Column<int>(type: "int", nullable: true),
                    vacation_from_the_pharmacy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    indications = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    producer = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    expiration_date = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    brand_name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    quantity_in_pack = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Filterr__26BE28459F97D3DC", x => x.Id_categories);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Namee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone_number = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__A949FB8DB54D7456", x => x.User_number);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Number_product = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_categories = table.Column<int>(type: "int", nullable: false),
                    Namee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Product_price = table.Column<decimal>(type: "decimal(25,2)", nullable: false),
                    Product_description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Article = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false, defaultValueSql: "(CONVERT([varchar](255),newid()))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Product__AA6BD16AA12F16DF", x => x.Number_product);
                    table.ForeignKey(
                        name: "FK__Product__Id_cate__3C69FB99",
                        column: x => x.Id_categories,
                        principalTable: "Filterr",
                        principalColumn: "Id_categories");
                });

            migrationBuilder.CreateTable(
                name: "Saved_address",
                columns: table => new
                {
                    User_idd = table.Column<int>(type: "int", nullable: false),
                    Address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    House = table.Column<int>(type: "int", nullable: false),
                    Construction = table.Column<int>(type: "int", nullable: false),
                    Flat = table.Column<int>(type: "int", nullable: false),
                    Address_name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Saved_ad__7A0C9D06E6D0B9AA", x => new { x.User_idd, x.Address_id });
                    table.ForeignKey(
                        name: "FK__Saved_add__User___3F466844",
                        column: x => x.User_idd,
                        principalTable: "Users",
                        principalColumn: "User_number");
                });

            migrationBuilder.CreateTable(
                name: "Basket",
                columns: table => new
                {
                    User_idd = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false),
                    Quantity_of_goods = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Basket__73B78D03653F4218", x => new { x.User_idd, x.Product_id });
                    table.ForeignKey(
                        name: "FK__Basket__Product___4316F928",
                        column: x => x.Product_id,
                        principalTable: "Product",
                        principalColumn: "Number_product");
                    table.ForeignKey(
                        name: "FK__Basket__User_idd__4222D4EF",
                        column: x => x.User_idd,
                        principalTable: "Users",
                        principalColumn: "User_number");
                });

            migrationBuilder.CreateTable(
                name: "Orderr",
                columns: table => new
                {
                    Order_Number = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_idd = table.Column<int>(type: "int", nullable: false),
                    Number_product = table.Column<int>(type: "int", nullable: false),
                    Date_references = table.Column<DateTime>(type: "datetime", nullable: false),
                    Statuss = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Orderr__B8CE93343714D952", x => new { x.Order_Number, x.User_idd, x.Number_product });
                    table.ForeignKey(
                        name: "FK__Orderr__Number_p__46E78A0C",
                        column: x => x.Number_product,
                        principalTable: "Product",
                        principalColumn: "Number_product");
                    table.ForeignKey(
                        name: "FK__Orderr__User_idd__45F365D3",
                        column: x => x.User_idd,
                        principalTable: "Users",
                        principalColumn: "User_number");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Basket_Product_id",
                table: "Basket",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orderr_Number_product",
                table: "Orderr",
                column: "Number_product");

            migrationBuilder.CreateIndex(
                name: "IX_Orderr_User_idd",
                table: "Orderr",
                column: "User_idd");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Id_categories",
                table: "Product",
                column: "Id_categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basket");

            migrationBuilder.DropTable(
                name: "Orderr");

            migrationBuilder.DropTable(
                name: "Saved_address");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Filterr");
        }
    }
}
