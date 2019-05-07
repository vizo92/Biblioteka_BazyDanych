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
    public class czytelnicyController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: czytelnicy
        public ActionResult Index(string option, string search, int? page, string sort)
        {
            List<SelectListItem> searchOptions = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text=""},
                 new SelectListItem{ Value="Imię" ,Text="Imię"},
                 new SelectListItem{ Value="Nazwisko",Text="Nazwisko"},
                 new SelectListItem{ Value="Miasto",Text="Miasto"},
                 new SelectListItem{ Value="Ulica",Text="Ulica"},
                 new SelectListItem{ Value="Liczba książek",Text="Liczba książek"},
             };
            searchOptions = data.ToList();

            ViewBag.option = searchOptions;

            ViewBag.SortByID = string.IsNullOrEmpty(sort) ? "descending id" : "";
            ViewBag.SortByName = sort == "Imię" ? "descending imie" : "Imię";
            ViewBag.SortBySurname = sort == "Nazwisko" ? "descending nazwisko" : "Nazwisko";
            ViewBag.SortByCity = sort == "Miasto" ? "descending miasto" : "Miasto";
            ViewBag.SortByStreet = sort == "Ulica" ? "descending ulica" : "Liczba ulica";
            ViewBag.SortByBooks = sort == "Liczba książek" ? "descending books" : "Liczba książek";

            //here we are converting the db.autorzy to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.czytelnicy.AsQueryable();

            if (option == "Imię")
            {
                records = records.Where(x => x.imie == search || search == null);
            }
            else if (option == "Nazwisko")
            {
                records = records.Where(x => x.nazwisko == search || search == null);
            }
            else if (option == "Miasto")
            {
                records = records.Where(x => x.miastso == search || search == null);
            }
            else if (option == "Ulica")
            {
                records = records.Where(x => x.ulica == search || search == null);
            }
            else if (option == "Liczba książek")
            {
                records = records.Where(x => x.liczba_ksiazek.ToString() == search || search == null);
            }

            switch (sort)
            {
                case "descending id":
                    records = records.OrderByDescending(x => x.id_czytelnika);
                    break;

                case "descending imie":
                    records = records.OrderByDescending(x => x.imie);
                    break;

                case "Imię":
                    records = records.OrderBy(x => x.imie);
                    break;

                case "descending nazwisko":
                    records = records.OrderByDescending(x => x.nazwisko);
                    break;

                case "Nazwisko":
                    records = records.OrderBy(x => x.nazwisko);
                    break;

                case "descending miasto":
                    records = records.OrderByDescending(x => x.miastso);
                    break;

                case "Miasto":
                    records = records.OrderBy(x => x.miastso);
                    break;

                case "descending ulica":
                    records = records.OrderByDescending(x => x.ulica);
                    break;

                case "Ulica":
                    records = records.OrderBy(x => x.ulica);
                    break;
                case "descending books":
                    records = records.OrderByDescending(x => x.liczba_ksiazek);
                    break;

                case "Liczba książek":
                    records = records.OrderBy(x => x.liczba_ksiazek);
                    break;
                default:
                    records = records.OrderBy(x => x.id_czytelnika);
                    break;

            }
            return View(records.ToPagedList(page ?? 1, 10));

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
