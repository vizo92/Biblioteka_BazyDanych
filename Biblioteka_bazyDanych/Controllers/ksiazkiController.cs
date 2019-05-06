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
    public class ksiazkiController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: ksiazki
        public ActionResult Index()
        {
            var ksiazki = db.ksiazki.Include(k => k.autorzy).Include(k => k.gatunki).Include(k => k.wydawnictwa);
            return View(ksiazki.ToList());
        }

        // GET: ksiazki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ksiazki ksiazki = db.ksiazki.Find(id);
            if (ksiazki == null)
            {
                return HttpNotFound();
            }
            return View(ksiazki);
        }

        // GET: ksiazki/Create
        public ActionResult Create()
        {
            IEnumerable<SelectListItem> items = db.autorzy.Select(c => new SelectListItem
            {
                Value = c.id_autora.ToString(),
                Text = c.imie +" "+c.nazwisko

            });

            ViewBag.id_autora = items;
            ViewBag.gatunek = new SelectList(db.gatunki, "nazwa", "nazwa");
            ViewBag.wydawnictwo = new SelectList(db.wydawnictwa, "nazwa", "nazwa");
            return View();
        }

        // POST: ksiazki/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_ksiazki,id_autora,wydawnictwo,gatunek,tytul")] ksiazki ksiazki)
        {
            if (ModelState.IsValid)
            {
                db.ksiazki.Add(ksiazki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_autora = new SelectList(db.autorzy, "id_autora", "imie", ksiazki.id_autora);
            ViewBag.gatunek = new SelectList(db.gatunki, "nazwa", "nazwa", ksiazki.gatunek);
            ViewBag.wydawnictwo = new SelectList(db.wydawnictwa, "nazwa", "nazwa", ksiazki.wydawnictwo);
            return View(ksiazki);
        }

        // GET: ksiazki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ksiazki ksiazki = db.ksiazki.Find(id);
            if (ksiazki == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_autora = new SelectList(db.autorzy, "id_autora", "imie", ksiazki.id_autora);
            ViewBag.gatunek = new SelectList(db.gatunki, "nazwa", "nazwa", ksiazki.gatunek);
            ViewBag.wydawnictwo = new SelectList(db.wydawnictwa, "nazwa", "kraj", ksiazki.wydawnictwo);
            return View(ksiazki);
        }

        // POST: ksiazki/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_ksiazki,id_autora,wydawnictwo,gatunek,tytul")] ksiazki ksiazki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ksiazki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_autora = new SelectList(db.autorzy, "id_autora", "imie", ksiazki.id_autora);
            ViewBag.gatunek = new SelectList(db.gatunki, "nazwa", "nazwa", ksiazki.gatunek);
            ViewBag.wydawnictwo = new SelectList(db.wydawnictwa, "nazwa", "kraj", ksiazki.wydawnictwo);
            return View(ksiazki);
        }

        // GET: ksiazki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ksiazki ksiazki = db.ksiazki.Find(id);
            if (ksiazki == null)
            {
                return HttpNotFound();
            }
            return View(ksiazki);
        }

        // POST: ksiazki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ksiazki ksiazki = db.ksiazki.Find(id);
            db.ksiazki.Remove(ksiazki);
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
