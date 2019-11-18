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
    public class TipoProdutosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: TipoProdutos
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.TipoProdutos.Where(c => c.DESCRICAO.Contains(filtro)).ToListAsync());
                else
                    return View(await db.TipoProdutos.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: TipoProdutos/Details/5
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
                TipoProduto tipoProduto = await db.TipoProdutos.FindAsync(id);
                if (tipoProduto == null)
                {
                    return HttpNotFound();
                }
                return View(tipoProduto);
            }
        }

        // GET: TipoProdutos/Create
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

        // POST: TipoProdutos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDTIPOPRODUTO,DESCRICAO")] TipoProduto tipoProduto)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.TipoProdutos.Add(tipoProduto);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(tipoProduto);
            }
        }

        // GET: TipoProdutos/Edit/5
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
                TipoProduto tipoProduto = await db.TipoProdutos.FindAsync(id);
                if (tipoProduto == null)
                {
                    return HttpNotFound();
                }
                return View(tipoProduto);
            }
        }

        // POST: TipoProdutos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDTIPOPRODUTO,DESCRICAO")] TipoProduto tipoProduto)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tipoProduto).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(tipoProduto);
            }
        }

        // GET: TipoProdutos/Delete/5
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
                TipoProduto tipoProduto = await db.TipoProdutos.FindAsync(id);
                if (tipoProduto == null)
                {
                    return HttpNotFound();
                }
                return View(tipoProduto);
            }
        }

        // POST: TipoProdutos/Delete/5
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
                TipoProduto tipoProduto = await db.TipoProdutos.FindAsync(id);
                db.TipoProdutos.Remove(tipoProduto);
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
