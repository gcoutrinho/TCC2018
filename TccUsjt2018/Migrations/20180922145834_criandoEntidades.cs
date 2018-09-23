using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace TccUsjt2018.Migrations
{
    public partial class criandoEntidades : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaProduto",
                columns: table => new
                {
                    CodigoCategoria = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescricaoCategoria = table.Column<string>(nullable: true),
                    NomeCategoria = table.Column<string>(nullable: true),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaProduto", x => x.CodigoCategoria);
                });
            migrationBuilder.CreateTable(
                name: "Estoque",
                columns: table => new
                {
                    CodigoEstoque = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescricaoEstoque = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estoque", x => x.CodigoEstoque);
                });
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    CodigoProduto = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoriaId = table.Column<int>(nullable: false),
                    CategoriaProdutoCodigoCategoria = table.Column<int>(nullable: true),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    NomeProduto = table.Column<string>(nullable: true),
                    ValorProduto = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.CodigoProduto);
                    table.ForeignKey(
                        name: "FK_Produto_CategoriaProduto_CategoriaProdutoCodigoCategoria",
                        column: x => x.CategoriaProdutoCodigoCategoria,
                        principalTable: "CategoriaProduto",
                        principalColumn: "CodigoCategoria",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    CodigoLote = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    DescricaoLote = table.Column<string>(nullable: true),
                    ProdutoId = table.Column<int>(nullable: false),
                    QuantidadeProduto = table.Column<int>(nullable: false),
                    ValidadeLote = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.CodigoLote);
                    table.ForeignKey(
                        name: "FK_Lote_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "CodigoProduto",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Estoque");
            migrationBuilder.DropTable("Lote");
            migrationBuilder.DropTable("Produto");
            migrationBuilder.DropTable("CategoriaProduto");
        }
    }
}
