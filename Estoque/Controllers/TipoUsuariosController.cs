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
    public class TipoUsuariosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: TipoUsuarios
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.TipoUsuarios.Where(c => c.DESCRICAO.Contains(filtro)).ToListAsync());
                else
                    return View(await db.TipoUsuarios.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: TipoUsuarios/Details/5
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
                TipoUsuario tipoUsuario = await db.TipoUsuarios.FindAsync(id);
                if (tipoUsuario == null)
                {
                    return HttpNotFound();
                }
                return View(tipoUsuario);
            }
        }

        // GET: TipoUsuarios/Create
        public ActionResult Create()
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                return View();
            }
        }

        // POST: TipoUsuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOUSUARIO,DESCRICAO,ADMINISTRATIVO,ALTERA")] TipoUsuario tipoUsuario)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.TipoUsuarios.Add(tipoUsuario);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(tipoUsuario);
            }
        }

        // GET: TipoUsuarios/Edit/5
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
                TipoUsuario tipoUsuario = await db.TipoUsuarios.FindAsync(id);
                if (tipoUsuario == null)
                {
                    return HttpNotFound();
                }
                return View(tipoUsuario);
            }
        }

        // POST: TipoUsuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOUSUARIO,DESCRICAO,ADMINISTRATIVO,ALTERA")] TipoUsuario tipoUsuario)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tipoUsuario).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(tipoUsuario);
            }
        }

        // GET: TipoUsuarios/Delete/5
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
                TipoUsuario tipoUsuario = await db.TipoUsuarios.FindAsync(id);
                if (tipoUsuario == null)
                {
                    return HttpNotFound();
                }
                return View(tipoUsuario);
            }
        }

        // POST: TipoUsuarios/Delete/5
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
                TipoUsuario tipoUsuario = await db.TipoUsuarios.FindAsync(id);
                db.TipoUsuarios.Remove(tipoUsuario);
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
