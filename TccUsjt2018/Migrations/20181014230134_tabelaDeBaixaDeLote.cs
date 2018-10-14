using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace TccUsjt2018.Migrations
{
    public partial class tabelaDeBaixaDeLote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Lote_Estoque_Estoque_CodigoEstoque", table: "Lote");
            migrationBuilder.DropForeignKey(name: "FK_Lote_Produto_Produto_CodigoProduto", table: "Lote");
            migrationBuilder.CreateTable(
                name: "Baixa",
                columns: table => new
                {
                    CodigoBaixa = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataBaixa = table.Column<DateTime>(nullable: false),
                    Lote_CodigoLote = table.Column<int>(nullable: false),
                    Produto_CodigoProduto = table.Column<int>(nullable: false),
                    QuantidadeBaixa = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baixa", x => x.CodigoBaixa);
                    table.ForeignKey(
                        name: "FK_Baixa_Lote_Lote_CodigoLote",
                        column: x => x.Lote_CodigoLote,
                        principalTable: "Lote",
                        principalColumn: "CodigoLote",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Baixa_Produto_Produto_CodigoProduto",
                        column: x => x.Produto_CodigoProduto,
                        principalTable: "Produto",
                        principalColumn: "CodigoProduto",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AlterColumn<int>(
                name: "Categoria_CodigoCategoria",
                table: "Produto",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Lote_Estoque_Estoque_CodigoEstoque",
                table: "Lote",
                column: "Estoque_CodigoEstoque",
                principalTable: "Estoque",
                principalColumn: "CodigoEstoque",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Lote_Produto_Produto_CodigoProduto",
                table: "Lote",
                column: "Produto_CodigoProduto",
                principalTable: "Produto",
                principalColumn: "CodigoProduto",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Lote_Estoque_Estoque_CodigoEstoque", table: "Lote");
            migrationBuilder.DropForeignKey(name: "FK_Lote_Produto_Produto_CodigoProduto", table: "Lote");
            migrationBuilder.DropTable("Baixa");
            migrationBuilder.AlterColumn<int>(
                name: "Categoria_CodigoCategoria",
                table: "Produto",
                nullable: false);
            migrationBuilder.AddForeignKey(
                name: "FK_Lote_Estoque_Estoque_CodigoEstoque",
                table: "Lote",
                column: "Estoque_CodigoEstoque",
                principalTable: "Estoque",
                principalColumn: "CodigoEstoque",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Lote_Produto_Produto_CodigoProduto",
                table: "Lote",
                column: "Produto_CodigoProduto",
                principalTable: "Produto",
                principalColumn: "CodigoProduto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
