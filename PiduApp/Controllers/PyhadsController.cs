using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PiduApp.Models;

namespace PiduApp.Controllers
{
    public class PyhadsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Pyhads
        public ActionResult Index()
        {
            return View(db.Pyhad.ToList());
        }

        // GET: Pyhads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pyha pyha = db.Pyhad.Find(id);
            if (pyha == null)
            {
                return HttpNotFound();
            }
            return View(pyha);
        }

        // GET: Pyhads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pyhads/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nimetus,Kuupaev")] Pyha pyha)
        {
            if (ModelState.IsValid)
            {
                db.Pyhad.Add(pyha);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pyha);
        }

        // GET: Pyhads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pyha pyha = db.Pyhad.Find(id);
            if (pyha == null)
            {
                return HttpNotFound();
            }
            return View(pyha);
        }

        // POST: Pyhads/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nimetus,Kuupaev")] Pyha pyha)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pyha).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pyha);
        }

        // GET: Pyhads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pyha pyha = db.Pyhad.Find(id);
            if (pyha == null)
            {
                return HttpNotFound();
            }
            return View(pyha);
        }

        // POST: Pyhads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pyha pyha = db.Pyhad.Find(id);
            db.Pyhad.Remove(pyha);
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
