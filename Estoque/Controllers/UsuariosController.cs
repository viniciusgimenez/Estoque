using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Estoque.Models;

namespace Estoque.Controllers
{
    public class UsuariosController : Controller
    {
        private Contexto db = new Contexto();
        Contexto bd = new Contexto();
        // GET: Usuarios
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.Usuarios.Where(c => c.NOME.Contains(filtro) || c.EMAIL.Contains(filtro)).ToListAsync());
                else
                    return View(await db.Usuarios.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = await db.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                List<TipoUsuario> tipoUsuarios = db.TipoUsuarios.Where(c => c.IDTIPOUSUARIO == usuario.IDTIPOUSUARIO).ToList();
                ViewBag.NomeTipoUsuario = tipoUsuarios[0].DESCRICAO;
                return View(usuario);
            }
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                ViewBag.TipoUsuarios = new SelectList(db.TipoUsuarios, "IDTIPOUSUARIO", "DESCRICAO");
                return View();
            }
        }

        // POST: Usuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDUSUARIO,NOME,EMAIL,SENHA,TELEFONE,IDTIPOUSUARIO")] Usuario usuario)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Usuarios.Add(usuario);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
        }

        // GET: Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = await db.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                ViewBag.TipoUsuarios = new SelectList(db.TipoUsuarios, "IDTIPOUSUARIO", "DESCRICAO");
                return View(usuario);
            }
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDUSUARIO,NOME,EMAIL,SENHA,TELEFONE,IDTIPOUSUARIO")] Usuario usuario)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(usuario).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
        }

        // GET: Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = await db.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                Usuario usuario = await db.Usuarios.FindAsync(id);
                db.Usuarios.Remove(usuario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
