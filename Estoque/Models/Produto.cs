using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class Produto
    {
        public Produto()
        {
            atributosProdutos = new HashSet<AtributoProduto>();
            movimentos = new HashSet<Movimento>();
        }        
        [Key]
        public int IDPRODUTO { get; set; }
        [Required]
        [Display(Name = "PRODUTO")]
        public string DESCRICAO { get; set; }
        [Required]
        [Display(Name = "FICHA TÉCNICA")]
        public string FICHATECNICA { get; set; }
        [Required]
        [Display(Name = "FABRICANTE")]
        public int IDFABRICANTE { get; set; }
        [Required]
        [Display(Name = "LINHA")]
        public int IDLINHA { get; set; }
        [Required]
        [Display(Name = "TIPO DE PRODUTO")]
        public int IDTIPOPRODUTO { get; set; }
        [Required]
        [Display(Name = "CATEGORIA")]
        public int IDCATEGORIA { get; set; }
        [Required]
        public decimal ESTOQUE { get; set; }
        [Required]
        public decimal VALOR { get; set; }
        [Required]
        [Display(Name = "VALOR DE VENDA")]
        public decimal VALORVENDA { get; set; }
        [ForeignKey("IDTIPOPRODUTO")]
        public List<TipoProduto> TipoProdutoCollection { get; set; }
        [ForeignKey("IDFABRICANTE")]
        public List<Fabricante> FabricanteCollection { get; set; }
        [ForeignKey("IDLINHA")]
        public List<Linha> LinhaCollection { get; set; }
        [ForeignKey("IDCATEGORIA")]
        public List<Categoria> CategoriaCollection { get; set; }
        public ICollection<AtributoProduto> atributosProdutos { get; set; }
        public ICollection<Movimento> movimentos { get; set; }

    }
}