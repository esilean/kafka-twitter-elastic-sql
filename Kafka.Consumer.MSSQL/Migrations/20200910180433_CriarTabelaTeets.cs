using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kafka.Consumer.MSSQL.Migrations
{
    public partial class CriarTabelaTeets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tweets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    FullText = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tweets");
        }
    }
}
