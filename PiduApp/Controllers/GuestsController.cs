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

        // GET: Guests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Find(id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            ViewBag.PyhaId = new SelectList(db.Pyhad, "Id", "Nimetus", guest.PyhaId);
            return View(guest);
        }

        // POST: Guests/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guest guest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PyhaId = new SelectList(db.Pyhad, "Id", "Nimetus", guest.PyhaId);
            return View(guest);
        }

        // GET: Guests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Guest guest = db.Guests.Include(g => g.Pyha).FirstOrDefault(g => g.Id == id);
            if (guest == null)
            {
                return HttpNotFound();
            }
            return View(guest);
        }

        // POST: Guests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Guest guest = db.Guests.Find(id);
            db.Guests.Remove(guest);
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