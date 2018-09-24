using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using TccUsjt2018.Database.EntitiesContext;

namespace TccUsjt2018.Migrations
{
    [DbContext(typeof(EntidadesContext))]
    [Migration("20180923233932_alteraBanco")]
    partial class alteraBanco
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("DescricaoLote");

                    b.Property<int>("Estoque_CodigoEstoque");

                    b.Property<int>("Produto_CodigoProduto");

                    b.Property<int>("QuantidadeProduto");

                    b.Property<DateTime>("ValidadeLote");

                    b.HasKey("CodigoLote");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Produto", b =>
                {
                    b.Property<int>("CodigoProduto")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Categoria_CodigoCategoria");

                    b.Property<DateTime>("DataCadastro");

                    b.Property<string>("MarcaProduto");

                    b.Property<string>("NomeProduto");

                    b.HasKey("CodigoProduto");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Lote", b =>
                {
                    b.HasOne("TccUsjt2018.Database.Entities.Estoque")
                        .WithMany()
                        .HasForeignKey("Estoque_CodigoEstoque");

                    b.HasOne("TccUsjt2018.Database.Entities.Produto")
                        .WithMany()
                        .HasForeignKey("Produto_CodigoProduto");
                });

            modelBuilder.Entity("TccUsjt2018.Database.Entities.Produto", b =>
                {
                    b.HasOne("TccUsjt2018.Database.Entities.CategoriaProduto")
                        .WithMany()
                        .HasForeignKey("Categoria_CodigoCategoria");
                });
        }
    }
}
