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
    public class MovimentoesController : Controller
    {
        private Contexto db = new Contexto();
        private int idprod = 0;
        // GET: Movimentoes
        public async Task<ActionResult> Index(string IDPRODUTO)
        {
            if (Session.Count > 0)
            {
                if (String.IsNullOrEmpty(IDPRODUTO))
                {
                    IDPRODUTO = Session["IDPROD"].ToString();
                }
                if (!String.IsNullOrEmpty(IDPRODUTO))
                {
                    if (!String.IsNullOrEmpty(IDPRODUTO))
                        idprod = Convert.ToInt32(IDPRODUTO);
                    else
                        idprod = Convert.ToInt32(TempData["idprod"]);
                    TempData["idprod"] = idprod;
                    Session["IDPROD"] = idprod.ToString();
                    ViewBag.Produtos = new SelectList(db.Produtoes.Where(c => c.IDPRODUTO == idprod), "IDPRODUTO", "DESCRICAO");
                    return View(await db.Movimentoes.Where(c => c.IDPRODUTO == idprod).ToListAsync());
                }
                else
                    return View(await db.Movimentoes.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: Movimentoes/Details/5
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
                Movimento movimento = await db.Movimentoes.FindAsync(id);
                if (movimento == null)
                {
                    return HttpNotFound();
                }
                List<Produto> produtos = db.Produtoes.Where(c => c.IDPRODUTO == movimento.IDPRODUTO).ToList();
                ViewBag.Produto = produtos[0].DESCRICAO;
                List<TipoMovimento> tipoMovimentos = db.TipoMovimentoes.Where(c => c.IDTIPOMOVIMENTO == movimento.IDTIPOMOVIMENTO).ToList();
                ViewBag.TipoMovimento = tipoMovimentos[0].DESCRICAO;
                List<Usuario> usuarios = db.Usuarios.Where(c => c.IDUSUARIO == movimento.IDUSUARIO).ToList();
                ViewBag.Usuario = usuarios[0].NOME;

                return View(movimento);
            }
        }

        // GET: Movimentoes/Create
        public ActionResult Create()
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (Session["ADM"] == "S")
                {
                    ViewBag.TipoMovimento = new SelectList(db.TipoMovimentoes, "IDTIPOMOVIMENTO", "DESCRICAO");
                    ViewBag.Usuario = new SelectList(db.Usuarios, "IDUSUARIO", "NOME");
                }
                else
                {
                    int idusuario = Convert.ToInt32(Session["ID"].ToString());
                    ViewBag.Usuario = new SelectList(db.Usuarios.Where(c => c.IDUSUARIO == idusuario), "IDUSUARIO", "NOME");
                    ViewBag.TipoMovimento = new SelectList(db.TipoMovimentoes.Where(c => c.ENTRADA == false), "IDTIPOMOVIMENTO", "DESCRICAO");
                }
                idprod = Convert.ToInt32(TempData["idprod"]);
                ViewBag.Produto = new SelectList(db.Produtoes.Where(c => c.IDPRODUTO == idprod), "IDPRODUTO", "DESCRICAO");

                return View();
            }
        }

        // POST: Movimentoes/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDMOVIMENTO,IDTIPOMOVIMENTO,IDPRODUTO,QUANTIDADE,IDUSUARIO")] Movimento movimento)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    TipoMovimento tipoMovimento = await db.TipoMovimentoes.FindAsync(movimento.IDTIPOMOVIMENTO);
                    Produto produto = await db.Produtoes.FindAsync(movimento.IDPRODUTO);
                    if (tipoMovimento.ENTRADA)
                        produto.ESTOQUE = produto.ESTOQUE + movimento.QUANTIDADE;
                    else
                        produto.ESTOQUE = produto.ESTOQUE - movimento.QUANTIDADE;
                    movimento.DATA = DateTime.Now;
                    db.Movimentoes.Add(movimento);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(movimento);
            }
        }

        // GET: Movimentoes/Edit/5
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
                Movimento movimento = await db.Movimentoes.FindAsync(id);
                if (movimento == null)
                {
                    return HttpNotFound();
                }
                return View(movimento);
            }
        }

        // POST: Movimentoes/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDMOVIMENTO,IDTIPOMOVIMENTO,IDPRODUTO,QUANTIDADE,IDUSUARIO")] Movimento movimento)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(movimento).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(movimento);
            }
        }

        // GET: Movimentoes/Delete/5
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
                Movimento movimento = await db.Movimentoes.FindAsync(id);
                if (movimento == null)
                {
                    return HttpNotFound();
                }
                List<Produto> produtos = db.Produtoes.Where(c => c.IDPRODUTO == movimento.IDPRODUTO).ToList();
                ViewBag.Produto = produtos[0].DESCRICAO;
                List<TipoMovimento> tipoMovimentos = db.TipoMovimentoes.Where(c => c.IDTIPOMOVIMENTO == movimento.IDTIPOMOVIMENTO).ToList();
                ViewBag.TipoMovimento = tipoMovimentos[0].DESCRICAO;
                List<Usuario> usuarios = db.Usuarios.Where(c => c.IDUSUARIO == movimento.IDUSUARIO).ToList();
                ViewBag.Usuario = usuarios[0].NOME;
                return View(movimento);
            }
        }

        // POST: Movimentoes/Delete/5
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
                Movimento movimento = await db.Movimentoes.FindAsync(id);
                db.Movimentoes.Remove(movimento);
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
