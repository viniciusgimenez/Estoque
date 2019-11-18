using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Estoque.Models
{
    public class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }
        [Key]
        public int IDTIPOUSUARIO { get; set; }
        [Required]
        [MaxLength(50)]
        [Display(Name = "TIPO DE USUÁRIO")]
        public string DESCRICAO { get; set; }
        [Required]
        public bool ADMINISTRATIVO { get; set; }
        [Required]
        public bool ALTERA { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
    }
}