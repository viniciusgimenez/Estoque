using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class Movimento
    {
        [Key]
        public int IDMOVIMENTO { get; set; }
        [Required]
        [Display(Name = "TIPO DE MOVIMENTO")]
        public int IDTIPOMOVIMENTO { get; set; }
        [Display(Name = "PRODUTO")]
        public int IDPRODUTO { get; set; }
        public decimal QUANTIDADE { get; set; }
        [Display(Name = "USUÁRIO")]
        public int IDUSUARIO { get; set; }
        public DateTime DATA { get; set; }

        [ForeignKey("IDTIPOMOVIMENTO")]
        public List<TipoMovimento> TipoMovimentos { get; set; }
        [ForeignKey("IDPRODUTO")]
        public List<Produto> Produtos { get; set; }
        [ForeignKey("IDUSUARIO")]
        public List<Usuario> Usuarios { get; set; }
    }
}