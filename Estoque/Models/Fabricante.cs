using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class Fabricante
    {
        public Fabricante()
        {
            Produtos = new HashSet<Produto>();
        }
        [Key]
        public int IDFABRICANTE { get; set; }
        [Required]
        [MaxLength(50)]
        public string NOME { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}