using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Estoque.Models
{
    public class Contexto : DbContext
    {
        public Contexto() : base("Contexto")
        {
        }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Linha> Linhas { get; set; }
        public DbSet<Fabricante> Fabricantes { get; set; }
        public DbSet<Atributo> Atributos{ get; set; }
        public DbSet<TipoProduto> TipoProdutos { get; set; }
        public DbSet<Categoria> Categorias{ get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<Estoque.Models.Produto> Produtoes { get; set; }

        public System.Data.Entity.DbSet<Estoque.Models.AtributoProduto> AtributosProdutoes { get; set; }

        public System.Data.Entity.DbSet<Estoque.Models.TipoMovimento> TipoMovimentoes { get; set; }

        public System.Data.Entity.DbSet<Estoque.Models.Movimento> Movimentoes { get; set; }
    }
}