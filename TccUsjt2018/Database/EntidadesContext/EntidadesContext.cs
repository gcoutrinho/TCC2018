using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using TccUsjt2018.Database.Entities;

namespace TccUsjt2018.Database.EntitiesContext
{
    public class EntidadesContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<CategoriaProduto> Categorias { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Seta o valor da minha connection strings que esta no Web.config
            string connectionString = ConfigurationManager.ConnectionStrings["banco23092018ConnectionString"].ConnectionString;
            //Seta o tipo de BD que sera usado para armazenar as informações
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}