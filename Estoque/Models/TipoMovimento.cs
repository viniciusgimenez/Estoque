using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class TipoMovimento
    {
        [Key]
        public int IDTIPOMOVIMENTO { get; set; }
        [Required]
        [Display(Name = "TIPO DE MOVIMENTO")]
        public string DESCRICAO { get; set; }
        [Required]
        public bool ENTRADA { get; set; }
    }
}