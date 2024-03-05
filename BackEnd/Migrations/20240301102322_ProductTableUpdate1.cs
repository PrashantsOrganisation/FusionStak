using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FusionStackBackEnd.Migrations
{
    public partial class ProductTableUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "products",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                table: "products",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IE2QP4ymp2csitgzZf8pf.sE8a2br3DhGuqjwNHI/8/fmlREnompO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "products");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "products");

            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "products");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$yqbMDhylDAiPlOgNbKRfxem7/9/bMCjwtVZZeBcEDzwdpaA6OfpRG");
        }
    }
}
