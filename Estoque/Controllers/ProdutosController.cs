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
    public class ProdutosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Produtos
        public async Task<ActionResult> Index(string filtro)
        {
            if (Session.Count > 0)
            {
                if (!String.IsNullOrEmpty(filtro))
                    return View(await db.Produtoes.Where(c => c.DESCRICAO.Contains(filtro)).ToListAsync());
                else
                    return View(await db.Produtoes.ToListAsync());
            }
            else
                return RedirectToAction("Index", "Home"); ;
        }

        // GET: Produtos/Details/5
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
                Produto produto = await db.Produtoes.FindAsync(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                List<Categoria> categorias = db.Categorias.Where(c => c.IDCATEGORIA == produto.IDCATEGORIA).ToList();
                ViewBag.Categoria = categorias[0].DESCRICAO;
                List<Linha> linhas = db.Linhas.Where(c => c.IDLINHA == produto.IDLINHA).ToList();
                ViewBag.Linha = linhas[0].DESCRICAO;
                List<Fabricante> fabricantes = db.Fabricantes.Where(c => c.IDFABRICANTE == produto.IDFABRICANTE).ToList();
                ViewBag.Fabricante = fabricantes[0].NOME;
                List<TipoProduto> tipoProdutos = db.TipoProdutos.Where(c => c.IDTIPOPRODUTO == produto.IDTIPOPRODUTO).ToList();
                ViewBag.TipoProdutos = tipoProdutos[0].DESCRICAO;

                return View(produto);
            }
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                ViewBag.Categoria = new SelectList(db.Categorias, "IDCATEGORIA", "DESCRICAO");
                ViewBag.Linha = new SelectList(db.Linhas, "IDLINHA", "DESCRICAO");
                ViewBag.Fabricante = new SelectList(db.Fabricantes, "IDFABRICANTE", "NOME");
                ViewBag.TipoProdutos = new SelectList(db.TipoProdutos, "IDTIPOPRODUTO", "DESCRICAO");
                return View();
            }
        }

        // POST: Produtos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDPRODUTO,DESCRICAO,FICHATECNICA,IDFABRICANTE,IDLINHA,IDTIPOPRODUTO,IDCATEGORIA,ESTOQUE,VALOR,VALORVENDA")] Produto produto)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Produtoes.Add(produto);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(produto);
            }
        }

        // GET: Produtos/Edit/5
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
                Produto produto = await db.Produtoes.FindAsync(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Categoria = new SelectList(db.Categorias, "IDCATEGORIA", "DESCRICAO");
                ViewBag.Linha = new SelectList(db.Linhas, "IDLINHA", "DESCRICAO");
                ViewBag.Fabricante = new SelectList(db.Fabricantes, "IDFABRICANTE", "NOME");
                ViewBag.TipoProdutos = new SelectList(db.TipoProdutos, "IDTIPOPRODUTO", "DESCRICAO");
                return View(produto);
            }
        }

        // POST: Produtos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDPRODUTO,DESCRICAO,FICHATECNICA,IDFABRICANTE,IDLINHA,IDTIPOPRODUTO,IDCATEGORIA,ESTOQUE,VALOR,VALORVENDA")] Produto produto)
        {
            if (Session.Count == 0)
            {
                return RedirectToAction("Index", "Home"); ;
            }
            else
            {
                if (ModelState.IsValid)
                {
                    db.Entry(produto).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(produto);
            }
        }

        // GET: Produtos/Delete/5
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
                Produto produto = await db.Produtoes.FindAsync(id);
                if (produto == null)
                {
                    return HttpNotFound();
                }
                List<Categoria> categorias = db.Categorias.Where(c => c.IDCATEGORIA == produto.IDCATEGORIA).ToList();
                ViewBag.Categoria = categorias[0].DESCRICAO;
                List<Linha> linhas = db.Linhas.Where(c => c.IDLINHA == produto.IDLINHA).ToList();
                ViewBag.Linha = linhas[0].DESCRICAO;
                List<Fabricante> fabricantes = db.Fabricantes.Where(c => c.IDFABRICANTE == produto.IDFABRICANTE).ToList();
                ViewBag.Fabricante = fabricantes[0].NOME;
                List<TipoProduto> tipoProdutos = db.TipoProdutos.Where(c => c.IDTIPOPRODUTO == produto.IDTIPOPRODUTO).ToList();
                ViewBag.TipoProdutos = tipoProdutos[0].DESCRICAO;
                return View(produto);
            }
        }

        // POST: Produtos/Delete/5
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
                Produto produto = await db.Produtoes.FindAsync(id);
                db.Produtoes.Remove(produto);
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
