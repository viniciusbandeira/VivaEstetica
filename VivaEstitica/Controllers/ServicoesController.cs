using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VivaEstitica.Models;
using VivaEstitica.ViewModels;

namespace VivaEstitica.Controllers
{
    public class ServicoesController : Controller
    {
        private VivaEsteticaContext db = new VivaEsteticaContext();



        // GET: Servicoes
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                return View(db.Servicos.ToList().Where(s => s.Cliente.Documento.Equals(((DadosLogin)Session["user"]).documento)));
            }
            else
            {
                return RedirectToAction("Login", "Home", null);
            }
        }

        // GET: Servicoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servicos.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            return View(servico);
        }

        public ActionResult TimeError()
        {
            return View();
        }

        public ActionResult CancelError()
        {
            return View();
        }

        public ActionResult MassageCancelError()
        {
            return View();
        }

        // GET: Servicoes/Create
        public ActionResult Create()
        {
            if(DateTime.Now.Hour>15 || DateTime.Now.Hour < 8)
            {
                return RedirectToAction("TimeError");
            }
            Dictionary<int, string> listaProfissionais = new Dictionary<int, string>();
            db.Profissionais.ToList().ForEach(p => listaProfissionais.Add(p.Id, p.Formacao+" - "+p.Nome));
            ViewBag.listaProfissionais = listaProfissionais;

            Dictionary<int, string> listaTipo = new Dictionary<int, string>();
            db.TipoServicos.ToList().ForEach(p => listaTipo.Add(p.Id, p.Descricao +" - Duração:"+ p.DuracaoMinutos+"Minitos - Valor:" + p.valor));
            ViewBag.listaTipo = listaTipo;

            return View();
        }

        // POST: Servicoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Inicio")] Servico servico)
        {
            if (DateTime.Now.Hour > 15 || DateTime.Now.Hour < 8)
            {
                return RedirectToAction("TimeError");
            }
            try
            {
                DadosLogin dados = (DadosLogin)Session["user"];

                servico.Cliente = db.Clientes.FirstOrDefault(c => c.Documento.Equals(dados.documento));
                servico.Profissional = db.Profissionais.Find(int.Parse(Request.Form["profissional"]));
                servico.TipoServico = db.TipoServicos.Find(int.Parse(Request.Form["tipo"]));
                servico.Estado = Estados.aguardando;
                db.Servicos.Add(servico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }catch(Exception e)
            {

            }

            Dictionary<int, string> listaProfissionais = new Dictionary<int, string>();
            db.Profissionais.ToList().ForEach(p => listaProfissionais.Add(p.Id, p.Formacao + " - " + p.Nome));
            ViewBag.listaProfissionais = listaProfissionais;

            Dictionary<int, string> listaTipo = new Dictionary<int, string>();
            db.TipoServicos.ToList().ForEach(p => listaTipo.Add(p.Id, p.Descricao + " - Duração:" + p.DuracaoMinutos + "Minitos - Valor:" + p.valor));
            ViewBag.listaTipo = listaTipo;

            return View(servico);
        }

        // GET: Servicoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servicos.Find(id);
            if (servico.TipoServico.Descricao.Equals("Massagem"))
            {
                if ((DateTime.Now - servico.Inicio).Hours > 24)
                {
                    return RedirectToAction("MassageCancelError");
                }
            }
            else
            {
                if (DateTime.Now.Day >= servico.Inicio.Day)
                {
                    return RedirectToAction("CancelError");
                }
            }
            if (servico == null)
            {
                return HttpNotFound();
            }

            Dictionary<int, string> listaProfissionais = new Dictionary<int, string>();
            db.Profissionais.ToList().ForEach(p => listaProfissionais.Add(p.Id, p.Formacao + " - " + p.Nome));
            ViewBag.listaProfissionais = listaProfissionais;

            return View(servico);
        }

        // POST: Servicoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Inicio, TipoServico")] Servico servico)
        {
            if (servico.TipoServico.Descricao.Equals("Massagem"))
            {
                if ((DateTime.Now - servico.Inicio).Hours > 24)
                {
                    return RedirectToAction("MassageCancelError");
                }
            }
            else
            {
                if (DateTime.Now.Day >= servico.Inicio.Day)
                {
                    return RedirectToAction("CancelError");
                }
            }
            try
            {
                servico.Profissional = db.Profissionais.Find(int.Parse(Request.Form["profissional"]));
                servico.Estado = Estados.aguardando;
                db.Entry(servico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

            }

            Dictionary<int, string> listaProfissionais = new Dictionary<int, string>();
            db.Profissionais.ToList().ForEach(p => listaProfissionais.Add(p.Id, p.Formacao + " - " + p.Nome));
            ViewBag.listaProfissionais = listaProfissionais;
            return View(servico);
        }

        // GET: Servicoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servico servico = db.Servicos.Find(id);
            if (servico == null)
            {
                return HttpNotFound();
            }
            if (servico.TipoServico.Descricao.Equals("Massagem"))
            {
                if((DateTime.Now - servico.Inicio).Hours>24)
                {
                    return RedirectToAction("MassageCancelError");
                }
            }
            else
            {
                if (DateTime.Now.Day >=servico.Inicio.Day)
                {
                    return RedirectToAction("CancelError");
                }
            }

            return View(servico);
        }

        // POST: Servicoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servico servico = db.Servicos.Find(id);
            if (servico.TipoServico.Descricao.Equals("Massagem"))
            {
                if ((DateTime.Now - servico.Inicio).Hours > 24)
                {
                    return RedirectToAction("MassageCancelError");
                }
            }
            else
            {
                if (DateTime.Now.Day >= servico.Inicio.Day)
                {
                    return RedirectToAction("CancelError");
                }
            }
            db.Servicos.Remove(servico);
            db.SaveChanges();
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
