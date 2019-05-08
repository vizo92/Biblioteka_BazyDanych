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
    public class wydawnictwaController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: wydawnictwa
        public ActionResult Index(string option, string search, int? page, string sort)
        {
            List<SelectListItem> searchOptions = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text=""},
                 new SelectListItem{ Value="Nazwa" ,Text="Nazwa"},
                 new SelectListItem{ Value="Kraj",Text="Kraj"},
                 new SelectListItem{ Value="Miasto",Text="Miasto"},
             };
            searchOptions = data.ToList();

            ViewBag.option = searchOptions;

            ViewBag.SortByName = sort == "Nazwa" ? "descending nazwa" : "Nazwa";
            //if the sort value is gender then we are initializing the value as descending gender  
            ViewBag.SortByCountry = sort == "Kraj" ? "descending kraj" : "Kraj";
            ViewBag.SortByCity = sort == "Miasto" ? "descending city" : "Miasto";

            var records = db.wydawnictwa.AsQueryable();

            if (option == "Nazwa")
            {
                records = records.Where(x => x.nazwa == search || search == null);
            }
            else if (option == "Miasto")
            {
                records = records.Where(x => x.miasto == search || search == null);
            }
            else if (option == "Kraj")
            {
                records = records.Where(x => x.kraj == search || search == null);
            }

       


            switch (sort)
            {
                case "descending nazwa":
                    records = records.OrderByDescending(x => x.nazwa);
                    break;

                case "Nazwa":
                    records = records.OrderBy(x => x.nazwa);
                    break;

                case "descending kraj":
                    records = records.OrderByDescending(x => x.kraj);
                    break;

                case "Kraj":
                    records = records.OrderBy(x => x.kraj);
                    break;

                case "descending miasto":
                    records = records.OrderByDescending(x => x.miasto);
                    break;

                case "Miasto":
                    records = records.OrderBy(x => x.miasto);
                    break;

                default:
                    records = records.OrderBy(x => x.nazwa);
                    break;

            }
            return View(records.ToPagedList(page ?? 1, 10));
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
