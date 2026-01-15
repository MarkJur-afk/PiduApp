using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using PiduApp.Models;

namespace PiduApp.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        [Authorize] // See rida tagab, et vastata saavad ainult registreeritud inimesed
        [HttpGet]
        public ActionResult Ankeet()
        {
            // Võtame andmebaasist kõik pühad
            var pyhad = db.Pyhad.ToList();
            
            // Paneme pühad ViewBag'i, et SelectList saaks neid kasutada
            ViewBag.Pyhad = new SelectList(pyhad, "Id", "Nimetus");
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Ankeet(Guest guest)
        {
            if (ModelState.IsValid)
            {
                db.Guests.Add(guest);
                db.SaveChanges();
                
                // Saada e-mail (näide ülaltoodud WebMail koodiga)
                SaadaEmail(guest); 
                
                return RedirectToAction("Thanks");
            }

            // Kui tekkis viga, laeme pühad uuesti
            ViewBag.Pyhad = new SelectList(db.Pyhad.ToList(), "Id", "Nimetus");
            return View(guest);
        }

        private void SaadaEmail(Guest guest)
        {
            WebMail.SmtpServer = "smtp.gmail.com";
            WebMail.SmtpPort = 587;
            WebMail.EnableSsl = true;
            WebMail.UserName = "markjurgennn@gmail.com";
            WebMail.Password = "yebx sypb jbrt uhos"; // Google App Password

            WebMail.Send(guest.Email, "Kutse kinnitus", 
                "Tere " + guest.Name + "! Sinu vastus on salvestatud.");
        }

        public ActionResult Thanks()
        {
            return View();
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