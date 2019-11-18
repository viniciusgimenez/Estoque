using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class AtributoProduto
    {
        [Key, Column(Order = 0)]
        [Display(Name ="CARACTERÍSTICA")]
        public int IDATRIBUTO { get; set; }
        [Key, Column(Order = 1)]
        [Display(Name = "PRODUTO")]
        public int IDPRODUTO { get; set; }
        [Required]
        public string VALOR { get; set; }
        [ForeignKey("IDPRODUTO")]
        public List<Produto> Produtos { get; set; }
        [ForeignKey("IDATRIBUTO")]
        public List<Atributo> Atributos { get; set; }
    }
}