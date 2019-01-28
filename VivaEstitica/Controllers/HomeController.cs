using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VivaEstitica.ViewModels;
using VivaEstitica.Models;

namespace VivaEstitica.Controllers
{
    public class HomeController : Controller
    {
        private VivaEsteticaContext db = new VivaEsteticaContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            var dadosLogin = new DadosLogin();
            return View(dadosLogin);
        }

        [HttpPost]
        public ActionResult Login(DadosLogin dadosLogin)
        {
            if (dadosLogin.Autentica(db))
            {
                Session["user"] = dadosLogin;
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Index");
        }
    }
}