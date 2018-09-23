using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace TccUsjt2018.Migrations
{
    public partial class aleracaoTabProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Lote_Produto_ProdutoId", table: "Lote");
            migrationBuilder.AddForeignKey(
                name: "FK_Lote_Produto_ProdutoId",
                table: "Lote",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "CodigoProduto",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Lote_Produto_ProdutoId", table: "Lote");
            migrationBuilder.AddForeignKey(
                name: "FK_Lote_Produto_ProdutoId",
                table: "Lote",
                column: "ProdutoId",
                principalTable: "Produto",
                principalColumn: "CodigoProduto",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
