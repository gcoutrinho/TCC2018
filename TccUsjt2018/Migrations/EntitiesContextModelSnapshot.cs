using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Migrations
{
    [DbContext(typeof(EntidadesContext))]
    partial class EntitiesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TccUsjt2018.Database.Entities.CategoriaProduto", b =>
                {
                    b.Property<int>("CodigoCategoria")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DescricaoCategoria");

                    b.Property<string>("NomeCategoria");

                    b.Property<int>("ProdutoId");

                    b.HasKey("CodigoCategoria");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Estoque", b =>
                {
                    b.Property<int>("CodigoEstoque")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("DescricaoEstoque");

                    b.HasKey("CodigoEstoque");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Lote", b =>
                {
                    b.Property<int>("CodigoLote")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("DescricaoLote");

                    b.Property<int>("ProdutoId");

                    b.Property<int>("QuantidadeProduto");

                    b.Property<DateTime>("ValidadeLote");

                    b.HasKey("CodigoLote");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Produto", b =>
                {
                    b.Property<int>("CodigoProduto")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoriaId");

                    b.Property<int?>("CategoriaProdutoCodigoCategoria");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("Marca");

                    b.Property<string>("NomeProduto");

                    b.Property<decimal>("ValorProduto");

                    b.HasKey("CodigoProduto");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Lote", b =>
                {
                    b.HasOne("TccUsjt2018.Database.Entities.Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Produto", b =>
                {
                    b.HasOne("TccUsjt2018.Database.Entities.CategoriaProduto")
                        .WithMany()
                        .HasForeignKey("CategoriaProdutoCodigoCategoria");
                });
        }
    }
}
