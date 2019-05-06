using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka_bazyDanych;

namespace Biblioteka_bazyDanych.Controllers
{
    public class autorzyController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: autorzy
        /*public ActionResult Index()
        {
            return View(db.autorzy.ToList());
        }*/

        public ActionResult Index(string option, string search)
        {
            List<SelectListItem> searchOptions = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text=""},
                 new SelectListItem{ Value="Imię" ,Text="Imię"},
                 new SelectListItem{ Value="Nazwisko",Text="Nazwisko"},
                 new SelectListItem{ Value="Narodowość",Text="Narodowość"},
             };
            searchOptions = data.ToList();

            ViewBag.option = searchOptions;
            //if a user choose the radio button option as Imię  
            if (option == "Imię")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.autorzy.Where(x => x.imie == search || search == null).ToList());
            }
            else if (option == "Nazwisko")
            {
                return View(db.autorzy.Where(x => x.nazwisko == search || search == null).ToList());
            }
            else if (option == "Narodowość")
            {
                return View(db.autorzy.Where(x => x.narodowosc == search || search == null).ToList());
            }
            else if (option == "")
            {
                return View(db.autorzy.ToList());
            }
            else
            {
                return View(db.autorzy.ToList());
            }
        }

        // GET: autorzy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autorzy autorzy = db.autorzy.Find(id);
            if (autorzy == null)
            {
                return HttpNotFound();
            }
            return View(autorzy);
        }

        // GET: autorzy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: autorzy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_autora,imie,nazwisko,narodowosc,liczba_dziel,zyciorys")] autorzy autorzy)
        {
            if (ModelState.IsValid)
            {
                db.autorzy.Add(autorzy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(autorzy);
        }

        // GET: autorzy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autorzy autorzy = db.autorzy.Find(id);
            if (autorzy == null)
            {
                return HttpNotFound();
            }
            return View(autorzy);
        }

        // POST: autorzy/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_autora,imie,nazwisko,narodowosc,liczba_dziel,zyciorys")] autorzy autorzy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(autorzy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(autorzy);
        }

        // GET: autorzy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            autorzy autorzy = db.autorzy.Find(id);
            if (autorzy == null)
            {
                return HttpNotFound();
            }
            return View(autorzy);
        }

        // POST: autorzy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            autorzy autorzy = db.autorzy.Find(id);
            db.autorzy.Remove(autorzy);
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
