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
    public class czytelnicyController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: czytelnicy
        public ActionResult Index()
        {
            return View(db.czytelnicy.ToList());
        }

        // GET: czytelnicy/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            czytelnicy czytelnicy = db.czytelnicy.Find(id);
            if (czytelnicy == null)
            {
                return HttpNotFound();
            }
            return View(czytelnicy);
        }

        // GET: czytelnicy/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: czytelnicy/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_czytelnika,imie,nazwisko,miastso,ulica,liczba_ksiazek")] czytelnicy czytelnicy)
        {
            if (ModelState.IsValid)
            {
                db.czytelnicy.Add(czytelnicy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(czytelnicy);
        }

        // GET: czytelnicy/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            czytelnicy czytelnicy = db.czytelnicy.Find(id);
            if (czytelnicy == null)
            {
                return HttpNotFound();
            }
            return View(czytelnicy);
        }

        // POST: czytelnicy/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_czytelnika,imie,nazwisko,miastso,ulica,liczba_ksiazek")] czytelnicy czytelnicy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(czytelnicy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(czytelnicy);
        }

        // GET: czytelnicy/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            czytelnicy czytelnicy = db.czytelnicy.Find(id);
            if (czytelnicy == null)
            {
                return HttpNotFound();
            }
            return View(czytelnicy);
        }

        // POST: czytelnicy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            czytelnicy czytelnicy = db.czytelnicy.Find(id);
            db.czytelnicy.Remove(czytelnicy);
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
