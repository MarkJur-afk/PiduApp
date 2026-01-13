using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PiduApp.Models;

namespace PiduApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GuestsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Kuvab kõik külalised ja nende valitud pühad
        public ActionResult Index()
        {
            // .Include(g => g.Pyha) ütleb andmebaasile: "Võta pühade andmed ka kaasa!"
            var guests = db.Guests.Include(g => g.Pyha).ToList();
            return View(guests);
        }

        // Filtreeritud vaade: Tulevad
        public ActionResult Tulevad()
        {
            var tulevad = db.Guests.Include(g => g.Pyha)
                                   .Where(g => g.WillAttend == true)
                                   .ToList();
            ViewBag.Filter = "Tulevad külalised";
            return View("Index", tulevad);
        }

        // Filtreeritud vaade: Ei tule
        public ActionResult EiTule()
        {
            var eiTule = db.Guests.Include(g => g.Pyha)
                                   .Where(g => g.WillAttend == false)
                                   .ToList();
            ViewBag.Filter = "Mitteosalejad";
            return View("Index", eiTule);
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