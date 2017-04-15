using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SyF.Migrations
{
    public partial class testParse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Recipes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FromPage",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ToPage",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "FromPage",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ToPage",
                table: "Recipes");
        }
    }
}
