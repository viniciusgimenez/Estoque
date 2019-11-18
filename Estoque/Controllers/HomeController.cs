using Estoque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Estoque.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session.Clear();
            return View();
        }
        public ActionResult Principal(string msg)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (!String.IsNullOrEmpty(msg))
                    Content("<script language='javascript' type='text/javascript'>alert('Usuário sem permissão de acesso!');</script>");
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Usuario u)
        {
            // esta action trata o post (login)
            if (ModelState.IsValid) //verifica se é válido
            {
                using (Contexto bd = new Contexto())
                {
                    var usu = bd.Usuarios.Where(a => a.EMAIL.Equals(u.EMAIL) && a.SENHA.Equals(u.SENHA)).FirstOrDefault();
                    if (u != null)
                    {
                        Session["ID"] = usu.IDUSUARIO.ToString();
                        Session["NOME"] = usu.NOME.ToString();
                        var tu = bd.TipoUsuarios.Where(a => a.IDTIPOUSUARIO.Equals(usu.IDTIPOUSUARIO)).FirstOrDefault();
                        if (tu != null && tu.ADMINISTRATIVO)
                            Session["ADM"] = "S";
                        else
                            Session["ADM"] = "N";
                        return RedirectToAction("Principal", "Home"); ;
                    }
                }
            }
            return View(u);
        }
    }
   
}