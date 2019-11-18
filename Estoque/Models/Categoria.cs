using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class Categoria
    {
        public Categoria()
        {
            Produtos = new HashSet<Produto>();
        }
        [Key]
        public int IDCATEGORIA { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "CATEGORIA")]
        public string DESCRICAO { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}