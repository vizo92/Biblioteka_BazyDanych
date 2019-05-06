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
    public class wydawnictwaController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: wydawnictwa
        public ActionResult Index()
        {
            return View(db.wydawnictwa.ToList());
        }

        // GET: wydawnictwa/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wydawnictwa wydawnictwa = db.wydawnictwa.Find(id);
            if (wydawnictwa == null)
            {
                return HttpNotFound();
            }
            return View(wydawnictwa);
        }

        // GET: wydawnictwa/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: wydawnictwa/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nazwa,kraj,miasto")] wydawnictwa wydawnictwa)
        {
            if (ModelState.IsValid)
            {
                db.wydawnictwa.Add(wydawnictwa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(wydawnictwa);
        }

        // GET: wydawnictwa/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wydawnictwa wydawnictwa = db.wydawnictwa.Find(id);
            if (wydawnictwa == null)
            {
                return HttpNotFound();
            }
            return View(wydawnictwa);
        }

        // POST: wydawnictwa/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nazwa,kraj,miasto")] wydawnictwa wydawnictwa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wydawnictwa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(wydawnictwa);
        }

        // GET: wydawnictwa/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            wydawnictwa wydawnictwa = db.wydawnictwa.Find(id);
            if (wydawnictwa == null)
            {
                return HttpNotFound();
            }
            return View(wydawnictwa);
        }

        // POST: wydawnictwa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            wydawnictwa wydawnictwa = db.wydawnictwa.Find(id);
            db.wydawnictwa.Remove(wydawnictwa);
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
