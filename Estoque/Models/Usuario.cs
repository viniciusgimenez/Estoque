using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estoque.Models
{
    public class Usuario
    {
        [Key]
        public int IDUSUARIO { get; set; }
        [MaxLength(100)]
        public string NOME { get; set; }
        [EmailAddress]
        [MaxLength(200)]
        public string EMAIL { get; set; }
        [MaxLength(100)]
        public string SENHA { get; set; }
        [MaxLength(15)]
        public string TELEFONE { get; set; }
        [Display(Name = "TIPO DE USUÁRIO")]
        public int IDTIPOUSUARIO { get; set; }
        [ForeignKey("IDTIPOUSUARIO")]
        public List<TipoUsuario> TipoUsuarioCollection { get; set; }

    }
}