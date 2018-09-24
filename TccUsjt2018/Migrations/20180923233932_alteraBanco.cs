using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.Metadata;

namespace TccUsjt2018.Migrations
{
    public partial class alteraBanco : Migration
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
                    NomeCategoria = table.Column<string>(nullable: true)
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
                    Categoria_CodigoCategoria = table.Column<int>(nullable: false),
                    DataCadastro = table.Column<DateTime>(nullable: false),
                    MarcaProduto = table.Column<string>(nullable: true),
                    NomeProduto = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.CodigoProduto);
                    table.ForeignKey(
                        name: "FK_Produto_CategoriaProduto_Categoria_CodigoCategoria",
                        column: x => x.Categoria_CodigoCategoria,
                        principalTable: "CategoriaProduto",
                        principalColumn: "CodigoCategoria",
                        onDelete: ReferentialAction.Cascade);
                });
            migrationBuilder.CreateTable(
                name: "Lote",
                columns: table => new
                {
                    CodigoLote = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DescricaoLote = table.Column<string>(nullable: true),
                    Estoque_CodigoEstoque = table.Column<int>(nullable: false),
                    Produto_CodigoProduto = table.Column<int>(nullable: false),
                    QuantidadeProduto = table.Column<int>(nullable: false),
                    ValidadeLote = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lote", x => x.CodigoLote);
                    table.ForeignKey(
                        name: "FK_Lote_Estoque_Estoque_CodigoEstoque",
                        column: x => x.Estoque_CodigoEstoque,
                        principalTable: "Estoque",
                        principalColumn: "CodigoEstoque",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lote_Produto_Produto_CodigoProduto",
                        column: x => x.Produto_CodigoProduto,
                        principalTable: "Produto",
                        principalColumn: "CodigoProduto",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Lote");
            migrationBuilder.DropTable("Estoque");
            migrationBuilder.DropTable("Produto");
            migrationBuilder.DropTable("CategoriaProduto");
        }
    }
}
