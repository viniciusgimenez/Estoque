using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class TipoProduto
    {
        public TipoProduto()
        {
            Produtos = new HashSet<Produto>();
        }
        [Key]
        public int IDTIPOPRODUTO { get; set;}
        [Required]
        [MaxLength(50)]
        [Display(Name = "TIPO DE PRODUTO")]
        public string DESCRICAO { get;set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}