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
    public class wypozyczeniaController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: wypozyczenia
        public ActionResult Index()
        {
            var wypozyczenia = db.wypozyczenia.Include(w => w.czytelnicy).Include(w => w.ksiazki);
            return View(wypozyczenia.ToList());
        }

        // GET: wypozyczenia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wypozyczenia wypozyczenia = db.wypozyczenia.Find(id);
            if (wypozyczenia == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia);
        }

        // GET: wypozyczenia/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> items = db.czytelnicy.Select(c => new SelectListItem
            {
                Value = c.id_czytelnika.ToString(),
                Text = c.imie + " " + c.nazwisko

            });

            List<SelectListItem> statusList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="Zamówione",Text="Zamówione"},
                 new SelectListItem{ Value="Wypożyczone",Text="Wypożyczone"},
                 new SelectListItem{ Value="Zwrócone",Text="Zwrócone"},
             };
            statusList = data.ToList();

            ViewBag.id_czytelnika = items;
            ViewBag.id_ksiazki = new SelectList(db.ksiazki, "id_ksiazki", "tytul");
            ViewBag.status = statusList;
            return View();
        }

        // POST: wypozyczenia/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_wypozyczenia,id_czytelnika,id_ksiazki,data_zamowienia,data_wypozyczenia,data_zwrotu,status")] wypozyczenia wypozyczenia)
        {
            if (ModelState.IsValid)
            {
                db.wypozyczenia.Add(wypozyczenia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_czytelnika = new SelectList(db.czytelnicy, "id_czytelnika", "imie", wypozyczenia.id_czytelnika);
            ViewBag.id_ksiazki = new SelectList(db.ksiazki, "id_ksiazki", "wydawnictwo", wypozyczenia.id_ksiazki);
            return View(wypozyczenia);
        }

        // GET: wypozyczenia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wypozyczenia wypozyczenia = db.wypozyczenia.Find(id);
            if (wypozyczenia == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> statusList = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="Zamówione",Text="Zamówione"},
                 new SelectListItem{ Value="Wypożyczone",Text="Wypożyczone"},
                 new SelectListItem{ Value="Zwrócone",Text="Zwrócone"},
             };
            statusList = data.ToList();

            ViewBag.id_czytelnika = new SelectList(db.czytelnicy, "id_czytelnika", "imie", wypozyczenia.id_czytelnika);
            ViewBag.id_ksiazki = new SelectList(db.ksiazki, "id_ksiazki", "wydawnictwo", wypozyczenia.id_ksiazki);
            ViewBag.status = statusList;
            return View(wypozyczenia);
        }

        // POST: wypozyczenia/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_wypozyczenia,id_czytelnika,id_ksiazki,data_zamowienia,data_wypozyczenia,data_zwrotu,status")] wypozyczenia wypozyczenia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wypozyczenia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_czytelnika = new SelectList(db.czytelnicy, "id_czytelnika", "imie", wypozyczenia.id_czytelnika);
            ViewBag.id_ksiazki = new SelectList(db.ksiazki, "id_ksiazki", "wydawnictwo", wypozyczenia.id_ksiazki);
            return View(wypozyczenia);
        }

        // GET: wypozyczenia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wypozyczenia wypozyczenia = db.wypozyczenia.Find(id);
            if (wypozyczenia == null)
            {
                return HttpNotFound();
            }
            return View(wypozyczenia);
        }

        // POST: wypozyczenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            wypozyczenia wypozyczenia = db.wypozyczenia.Find(id);
            db.wypozyczenia.Remove(wypozyczenia);
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
