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
    public class TipoMovimentoesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: TipoMovimentoes
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.TipoMovimentoes.Where(c => c.DESCRICAO.Contains(filtro)).ToListAsync());
                else
                    return View(await db.TipoMovimentoes.ToListAsync());
            }
            else
                return View(await db.TipoMovimentoes.ToListAsync());
        }

        // GET: TipoMovimentoes/Details/5
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
                TipoMovimento tipoMovimento = await db.TipoMovimentoes.FindAsync(id);
                if (tipoMovimento == null)
                {
                    return HttpNotFound();
                }
                return View(tipoMovimento);
            }
        }

        // GET: TipoMovimentoes/Create
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

        // POST: TipoMovimentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOMOVIMENTO,DESCRICAO,ENTRADA")] TipoMovimento tipoMovimento)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.TipoMovimentoes.Add(tipoMovimento);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(tipoMovimento);
            }
        }

        // GET: TipoMovimentoes/Edit/5
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
                TipoMovimento tipoMovimento = await db.TipoMovimentoes.FindAsync(id);
                if (tipoMovimento == null)
                {
                    return HttpNotFound();
                }
                return View(tipoMovimento);
            }
        }

        // POST: TipoMovimentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOMOVIMENTO,DESCRICAO,ENTRADA")] TipoMovimento tipoMovimento)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tipoMovimento).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(tipoMovimento);
            }
        }

        // GET: TipoMovimentoes/Delete/5
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
                TipoMovimento tipoMovimento = await db.TipoMovimentoes.FindAsync(id);
                if (tipoMovimento == null)
                {
                    return HttpNotFound();
                }
                return View(tipoMovimento);
            }
        }

        // POST: TipoMovimentoes/Delete/5
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
                TipoMovimento tipoMovimento = await db.TipoMovimentoes.FindAsync(id);
                db.TipoMovimentoes.Remove(tipoMovimento);
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
