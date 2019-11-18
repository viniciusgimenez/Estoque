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
using System.Data.SqlClient;
using Estoque.Relatorios;
using CrystalDecisions.Shared;
using System.Web.UI;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;

namespace Estoque.Controllers
{
    public class AtributoProdutosController : Controller
    {
        private Contexto db = new Contexto();
        int idprod = 0;
        // GET: AtributoProdutos
        public async Task<ActionResult> Index(string IDPRODUTO)
        {
            if (String.IsNullOrEmpty(IDPRODUTO))
            {
                IDPRODUTO = Session["IDPROD"].ToString();
            }
            ViewBag.Produtos = new SelectList(db.Produtoes, "IDPRODUTO", "DESCRICAO");
            if (!String.IsNullOrEmpty(IDPRODUTO))
            {
                idprod = Convert.ToInt32(IDPRODUTO);
                TempData["idprod"] = idprod;
                Session["IDPROD"] = idprod.ToString();
                ViewBag.Produtos = new SelectList(db.Produtoes.Where(c => c.IDPRODUTO == idprod), "IDPRODUTO", "DESCRICAO");
                return View(await db.AtributosProdutoes.Where(c => c.IDPRODUTO == idprod).ToListAsync());
            }
            else
                return View(await db.AtributosProdutoes.ToListAsync());
        }
        public ActionResult ImprimirRel()
        {
            dsProdutoDet dt = new dsProdutoDet();
            SqlConnection conn = new SqlConnection("Data Source=localhost;Initial Catalog=ProjetoEstoqueLoja;Persist Security Info=True;User ID=net;Password=net123;MultipleActiveResultSets=True;");
            SqlCommand query = new SqlCommand();
            query.Connection = conn;
            query.CommandText = "select p.DESCRICAO as PRODUTO, FICHATECNICA, ESTOQUE, VALORVENDA, c.DESCRICAO as CATEGORIA, " +
"l.DESCRICAO as LINHA, tp.DESCRICAO as TIPOPRODUTO, a.DESCRICAO as CARACTERISTICA, ap.VALOR, f.NOME " +
"from Produto p " +
"inner join TipoProduto tp  on tp.IDTIPOPRODUTO = p.IDTIPOPRODUTO " +
"inner join Categoria c  on c.IDCATEGORIA = p.IDCATEGORIA " +
"inner join Linha l  on l.IDLINHA = p.IDLINHA " +
"inner join Fabricante f on f.IDFABRICANTE = p.IDFABRICANTE " +
"inner join AtributoProduto ap on ap.IDPRODUTO = p.IDPRODUTO " +
"inner join Atributo a on a.IDATRIBUTO = ap.IDATRIBUTO " +
"where p.IDPRODUTO = " + Session["IDPROD"].ToString() +
" order by p.IDPRODUTO ";
            DataRow r;
            DataTable t = dt.Tables.Add("dtProdutoDet");

            t.Columns.Add("PRODUTO", Type.GetType("System.String"));
            t.Columns.Add("FICHATECNICA", Type.GetType("System.String"));
            t.Columns.Add("ESTOQUE", Type.GetType("System.String"));
            t.Columns.Add("VALORVENDA", Type.GetType("System.String"));
            t.Columns.Add("CATEGORIA", Type.GetType("System.String"));
            t.Columns.Add("LINHA", Type.GetType("System.String"));
            t.Columns.Add("TIPOPRODUTO", Type.GetType("System.String"));
            t.Columns.Add("CARACTERISTICA", Type.GetType("System.String"));
            t.Columns.Add("VALOR", Type.GetType("System.String"));
            t.Columns.Add("NOME", Type.GetType("System.String"));
            conn.Open();
            SqlDataReader leitor = query.ExecuteReader();
            Stream stream = null;
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    r = t.NewRow();
                    r["PRODUTO"] = leitor["PRODUTO"].ToString();
                    r["FICHATECNICA"] = leitor["FICHATECNICA"].ToString();
                    r["ESTOQUE"] = leitor["ESTOQUE"].ToString();
                    r["VALORVENDA"] = leitor["ESTOQUE"].ToString();
                    r["CATEGORIA"] = leitor["CATEGORIA"].ToString();
                    r["LINHA"] = leitor["LINHA"].ToString();
                    r["TIPOPRODUTO"] = leitor["TIPOPRODUTO"].ToString();
                    r["CARACTERISTICA"] = leitor["CARACTERISTICA"].ToString();
                    r["VALOR"] = leitor["VALOR"].ToString();
                    r["NOME"] = leitor["NOME"].ToString();
                    t.Rows.Add(r);
                }
                conn.Close();
                RelProdDetalhado rd = new RelProdDetalhado();
                rd.Load(Server.MapPath("~/Relatorios/RelProdDetalhado.rpt"));

                rd.SetDataSource(dt);

                Response.Buffer = false;
                Response.ClearContent();
                Response.ClearHeaders();

                rd.ExportToDisk(ExportFormatType.PortableDocFormat, Server.MapPath("RelProdDet" + ".pdf"));                

            }
            string IDPRODUTO = null;
            idprod = Convert.ToInt32(IDPRODUTO);
            TempData["idprod"] = idprod;
            Session["IDPROD"] = idprod.ToString();
            ViewBag.Produtos = new SelectList(db.Produtoes.Where(c => c.IDPRODUTO == idprod), "IDPRODUTO", "DESCRICAO");
            return View(db.AtributosProdutoes.Where(c => c.IDPRODUTO == idprod).ToListAsync());
        }
        // GET: AtributoProdutos/Details/5
        public async Task<ActionResult> Details(int? id, int? idatributo)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtributoProduto atributoProduto = await db.AtributosProdutoes.FindAsync(id, idatributo);
            if (atributoProduto == null)
            {
                return HttpNotFound();
            }
            List<Atributo> atributos = db.Atributos.Where(c => c.IDATRIBUTO == idatributo).ToList();
            ViewBag.Atributo = atributos[0].DESCRICAO;
            List<Produto> produtos = db.Produtoes.Where(c => c.IDPRODUTO == id).ToList();
            ViewBag.Produto = produtos[0].DESCRICAO;
            return View(atributoProduto);
        }

        // GET: AtributoProdutos/Create
        public ActionResult Create()
        {
            idprod = Convert.ToInt32(TempData["idprod"]);
            ViewBag.Produto = new SelectList(db.Produtoes.Where(c => c.IDPRODUTO == idprod), "IDPRODUTO", "DESCRICAO");
            ViewBag.Atrbutos = new SelectList(db.Atributos, "IDATRIBUTO", "DESCRICAO");
            return View();
        }

        // POST: AtributoProdutos/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "IDATRIBUTO,IDPRODUTO,VALOR")] AtributoProduto atributoProduto)
        {
            if (ModelState.IsValid)
            {
                db.AtributosProdutoes.Add(atributoProduto);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(atributoProduto);
        }

        // GET: AtributoProdutos/Edit/5
        public async Task<ActionResult> Edit(int? id, int? idatributo)
        {
            if (id == null || idatributo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtributoProduto atributoProduto = await db.AtributosProdutoes.FindAsync(idatributo, id);
            if (atributoProduto == null)
            {
                return HttpNotFound();
            }
            List<Atributo> atributos = db.Atributos.Where(c => c.IDATRIBUTO == idatributo).ToList();
            ViewBag.Atributo = atributos[0].DESCRICAO;
            List<Produto> produtos = db.Produtoes.Where(c => c.IDPRODUTO == id).ToList();
            ViewBag.Produto = produtos[0].DESCRICAO;
            return View(atributoProduto);
        }

        // POST: AtributoProdutos/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "IDATRIBUTO,IDPRODUTO,VALOR")] AtributoProduto atributoProduto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(atributoProduto).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(atributoProduto);
        }

        // GET: AtributoProdutos/Delete/5
        public async Task<ActionResult> Delete(int? id, int? idatributo)
        {
            if (id == null || idatributo == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AtributoProduto atributoProduto = await db.AtributosProdutoes.FindAsync(id, idatributo);
            if (atributoProduto == null)
            {
                return HttpNotFound();
            }
            List<Atributo> atributos = db.Atributos.Where(c => c.IDATRIBUTO == idatributo).ToList();
            ViewBag.Atributo = atributos[0].DESCRICAO;
            List<Produto> produtos = db.Produtoes.Where(c => c.IDPRODUTO == id).ToList();
            ViewBag.Produto = produtos[0].DESCRICAO;
            return View(atributoProduto);
        }

        // POST: AtributoProdutos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AtributoProduto atributoProduto = await db.AtributosProdutoes.FindAsync(id);
            db.AtributosProdutoes.Remove(atributoProduto);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
