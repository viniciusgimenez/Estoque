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
    public class AtributosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Atributos
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (Session["ADM"] != "S")
                    return Content("<script language='javascript' type='text/javascript'>alert('Usuário sem permissão de acesso!');</script>");
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.Atributos.Where(c => c.DESCRICAO.Contains(filtro)).ToListAsync());
                else
                    return View(await db.Atributos.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: Atributos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] != "S")
                    return Content("<script language='javascript' type='text/javascript'>alert('Usuário sem permissão de acesso!');</script>");
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Atributo atributo = await db.Atributos.FindAsync(id);
                if (atributo == null)
                {
                    return HttpNotFound();
                }
                return View(atributo);
            }
        }

        // GET: Atributos/Create
        public ActionResult Create()
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] != "S")
                    return Content("<script language='javascript' type='text/javascript'>alert('Usuário sem permissão de acesso!');</script>");
                return View();
            }
        }

        // POST: Atributos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDATRIBUTO,DESCRICAO")] Atributo atributo)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] != "S")
                    return RedirectToAction("Principal", "Home");
                if (ModelState.IsValid)
                {
                    db.Atributos.Add(atributo);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(atributo);
            }
        }

        // GET: Atributos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] != "S")
                    return RedirectToAction("Principal", "Home");
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Atributo atributo = await db.Atributos.FindAsync(id);
                if (atributo == null)
                {
                    return HttpNotFound();
                }
                return View(atributo);
            }
        }

        // POST: Atributos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDATRIBUTO,DESCRICAO")] Atributo atributo)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] != "S")
                    return RedirectToAction("Principal", "Home");
                if (ModelState.IsValid)
                {
                    db.Entry(atributo).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(atributo);
            }
        }

        // GET: Atributos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] != "S")
                    return RedirectToAction("Principal", "Home");
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Atributo atributo = await db.Atributos.FindAsync(id);
                if (atributo == null)
                {
                    return HttpNotFound();
                }
                return View(atributo);
            }
        }

        // POST: Atributos/Delete/5
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
                if (Session["ADM"] != "S")
                {
                    return RedirectToAction("Principal", "Home");
                }
                Atributo atributo = await db.Atributos.FindAsync(id);
                db.Atributos.Remove(atributo);
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
