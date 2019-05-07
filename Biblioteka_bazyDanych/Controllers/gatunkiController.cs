using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteka_bazyDanych;
using PagedList;
using PagedList.Mvc;

namespace Biblioteka_bazyDanych.Controllers
{
    public class gatunkiController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: gatunki
        public ActionResult Index(string option, string search, int? page, string sort)
        {
            List<SelectListItem> searchOptions = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text=""},
                 new SelectListItem{ Value="Nazwa" ,Text="Nazwa"},
             };
            searchOptions = data.ToList();

            ViewBag.option = searchOptions;

            ViewBag.SortByName = string.IsNullOrEmpty(sort) ? "descending name" : "";

            //here we are converting the db.autorzy to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.gatunki.AsQueryable();

            if (option == "Nazwa")
            {
                records = records.Where(x => x.nazwa == search || search == null);
            }

            switch (sort)
            {
                case "descending name":
                    records = records.OrderByDescending(x => x.nazwa);
                    break;

                default:
                    records = records.OrderBy(x => x.nazwa);
                    break;

            }
            return View(records.ToPagedList(page ?? 1, 10));

        }

        // GET: gatunki/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gatunki gatunki = db.gatunki.Find(id);
            if (gatunki == null)
            {
                return HttpNotFound();
            }
            return View(gatunki);
        }

        // GET: gatunki/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: gatunki/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nazwa")] gatunki gatunki)
        {
            if (ModelState.IsValid)
            {
                db.gatunki.Add(gatunki);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gatunki);
        }

        // GET: gatunki/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gatunki gatunki = db.gatunki.Find(id);
            if (gatunki == null)
            {
                return HttpNotFound();
            }
            return View(gatunki);
        }

        // POST: gatunki/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "nazwa")] gatunki gatunki)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gatunki).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gatunki);
        }

        // GET: gatunki/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gatunki gatunki = db.gatunki.Find(id);
            if (gatunki == null)
            {
                return HttpNotFound();
            }
            return View(gatunki);
        }

        // POST: gatunki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            gatunki gatunki = db.gatunki.Find(id);
            db.gatunki.Remove(gatunki);
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
