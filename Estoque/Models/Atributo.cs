using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class Atributo
    {
        public Atributo()
        {
            atributosProdutos = new HashSet<AtributoProduto>();
        }
        [Key]
        public int IDATRIBUTO { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "DESCRIÇÃO")]
        public string DESCRICAO { get; set; }
        public ICollection<AtributoProduto> atributosProdutos { get; set; }
    }
}