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
    public class FabricantesController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Fabricantes
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.Fabricantes.Where(c => c.NOME.Contains(filtro)).ToListAsync());
                else
                    return View(await db.Fabricantes.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: Fabricantes/Details/5
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
                Fabricante fabricante = await db.Fabricantes.FindAsync(id);
                if (fabricante == null)
                {
                    return HttpNotFound();
                }
                return View(fabricante);
            }
        }

        // GET: Fabricantes/Create
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

        // POST: Fabricantes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDFABRICANTE,NOME")] Fabricante fabricante)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Fabricantes.Add(fabricante);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(fabricante);
            }
        }

        // GET: Fabricantes/Edit/5
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
                Fabricante fabricante = await db.Fabricantes.FindAsync(id);
                if (fabricante == null)
                {
                    return HttpNotFound();
                }
                return View(fabricante);
            }
        }

        // POST: Fabricantes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDFABRICANTE,NOME")] Fabricante fabricante)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(fabricante).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(fabricante);
            }
        }

        // GET: Fabricantes/Delete/5
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
                Fabricante fabricante = await db.Fabricantes.FindAsync(id);
                if (fabricante == null)
                {
                    return HttpNotFound();
                }
                return View(fabricante);
            }
        }

        // POST: Fabricantes/Delete/5
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
                Fabricante fabricante = await db.Fabricantes.FindAsync(id);
                db.Fabricantes.Remove(fabricante);
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
