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
    public class ksiazkiController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: ksiazki

        public ActionResult Index(string option, string search, int? page, string sort)
        {
            List<SelectListItem> searchOptions = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text=""},
                 new SelectListItem{ Value="Tytul" ,Text="Tytuł"},
                 new SelectListItem{ Value="Autor",Text="Autor"},
                 new SelectListItem{ Value="Gatunek",Text="Gatunek"},
                 new SelectListItem{ Value="Wydawnictwo",Text="Wydawnictwo"},
             };
            searchOptions = data.ToList();

            ViewBag.option = searchOptions;

            ViewBag.SortByID = string.IsNullOrEmpty(sort) ? "descending id" : "";
            ViewBag.SortByTitle = sort == "Tytul" ? "descending tytul" : "Tytul";
            ViewBag.SortByAuthor = sort == "Autor" ? "descending autor" : "Autor";
            ViewBag.SortByKind = sort == "Gatunek" ? "descending gatunek" : "Gatunek";
            ViewBag.SortByPress = sort == "Wydawnictwo" ? "descending wydawnictwo" : "Wydawnictwo";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "Status";

            //here we are converting the db.autorzy to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.ksiazki.Include(k => k.autorzy).Include(k => k.gatunki).Include(k => k.wydawnictwa).AsQueryable();

            if (option == "Tytul")
            {
                records = records.Where(x => x.tytul == search || search == null);
            }
            else if (option == "Autor")
            {
                records = records.Where(x => x.autorzy.imie + " "+x.autorzy.nazwisko == search || x.autorzy.nazwisko == search || x.autorzy.imie == search || search == null);
            }
            else if (option == "Gatunek")
            {
                records = records.Where(x => x.gatunek == search || search == null);
            }
            else if (option == "Wydawnictwo")
            {
                records = records.Where(x => x.wydawnictwo == search || search == null);
            }

            switch (sort)
            {
                case "descending id":
                    records = records.OrderByDescending(x => x.id_ksiazki);
                    break;

                case "descending tytul":
                    records = records.OrderByDescending(x => x.tytul);
                    break;

                case "Tytul":
                    records = records.OrderBy(x => x.tytul);
                    break;

                case "descending autor":
                    records = records.OrderByDescending(x => x.autorzy.nazwisko).ThenBy(x => x.autorzy.imie);
                    break;

                case "Autor":
                    records = records.OrderBy(x => x.autorzy.nazwisko).ThenBy(x => x.autorzy.imie);
                    break;

                case "descending gatunek":
                    records = records.OrderByDescending(x => x.gatunek);
                    break;

                case "Gatunek":
                    records = records.OrderBy(x => x.gatunek);
                    break;

                case "descending wydawnictwo":
                    records = records.OrderByDescending(x => x.wydawnictwo);
                    break;

                case "Wydawnictwo":
                    records = records.OrderBy(x => x.wydawnictwo);
                    break;
                case "descending status":
                    records = records.OrderByDescending(x => x.status);
                    break;

                case "Status":
                    records = records.OrderBy(x => x.status);
                    break;
                default:
                    records = records.OrderBy(x => x.id_ksiazki);
                    break;

            }
            return View(records.ToPagedList(page ?? 1, 10));

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
        public ActionResult Create([Bind(Include = "id_ksiazki,id_autora,wydawnictwo,gatunek,tytul,status")] ksiazki ksiazki)
        {
            if (ModelState.IsValid)
            {
                ksiazki.status = "Dostępna";   
                db.ksiazki.Add(ksiazki);
                UpdateBooksCount(ksiazki.id_autora);
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
        public ActionResult Edit([Bind(Include = "id_ksiazki,id_autora,wydawnictwo,gatunek,tytul,status")] ksiazki ksiazki)
        {

            if (ModelState.IsValid)
            {
                db.Entry(ksiazki).State = EntityState.Modified;
                UpdateBooksCount(ksiazki.id_autora);
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
            UpdateBooksCount(ksiazki.id_autora);
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

        public void UpdateBooksCount(int id_autora)
        {
            var modifyAuthor = db.autorzy.Where(x => x.id_autora == id_autora).FirstOrDefault();
            modifyAuthor.liczba_dziel = db.ksiazki.Where(x => x.id_autora == modifyAuthor.id_autora).Select(x => x.id_ksiazki).Count();
            db.Entry(modifyAuthor).State = EntityState.Modified;
        }
    }
}
