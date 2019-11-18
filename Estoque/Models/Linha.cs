using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class Linha
    {
        public Linha()
        {
            Produtos = new HashSet<Produto>();
        }
        [Key]
        public int IDLINHA { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "LINHA")]
        public string DESCRICAO { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}