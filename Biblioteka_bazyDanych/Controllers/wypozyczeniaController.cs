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
    public class wypozyczeniaController : Controller
    {
        private bibliotekaEntities1 db = new bibliotekaEntities1();

        // GET: wypozyczenia

        public ActionResult Index(string option, string search, int? page, string sort)
        {
            List<SelectListItem> searchOptions = new List<SelectListItem>();
            var data = new[]{
                 new SelectListItem{ Value="",Text=""},
                 new SelectListItem{ Value="DataZamowienia" ,Text="Data Zamówienia"},
                 new SelectListItem{ Value="DataWypozyczenia",Text="Data Wypożyczenia"},
                 new SelectListItem{ Value="DataZwrotu",Text="Data Zwrotu"},
                 new SelectListItem{ Value="Status",Text="Status"},
                 new SelectListItem{ Value="Czytelnik",Text="Czytelnik"},
                 new SelectListItem{ Value="Książka",Text="Książka"},
             };
            searchOptions = data.ToList();

            ViewBag.option = searchOptions;

            ViewBag.SortByID = string.IsNullOrEmpty(sort) ? "descending id" : "";
            ViewBag.SortByDateOrder = sort == "Data Zamówienia" ? "descending zamowienia" : "Data Zamówienia";
            ViewBag.SortByDateWith = sort == "Data Wypożyczenia" ? "descending wypozyczenia" : "Data Wypożyczenia";
            ViewBag.SortByDateTake = sort == "Data Zwrotu" ? "descending zwrotu" : "Data Zwrotu";
            ViewBag.SortByStatus = sort == "Status" ? "descending status" : "Status";
            ViewBag.SortByReader = sort == "Czytelnik" ? "descending czytelnik" : "Czytelnik";
            ViewBag.SortByBook = sort == "Książka" ? "descending ksiazka" : "Książka";

            //here we are converting the db.autorzy to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.wypozyczenia.Include(w => w.czytelnicy).Include(w => w.ksiazki).AsQueryable();

            if (option == "DataZamowienia")
            {
                records = records.Where(x => x.data_zamowienia.ToString() == search || search == null);
            }
            else if (option == "Czytelnik")
            {
                records = records.Where(x => x.czytelnicy.imie + " " + x.czytelnicy.nazwisko == search || x.czytelnicy.nazwisko == search || x.czytelnicy.imie == search || search == null);
            }
            else if (option == "DataWypozyczenia")
            {
                records = records.Where(x => x.data_wypozyczenia.ToString() == search || search == null);
            }
            else if (option == "DataZwrotu")
            {
                records = records.Where(x => x.data_zwrotu.ToString() == search || search == null);
            }
            else if (option == "Książka")
            {
                records = records.Where(x => x.ksiazki.tytul == search || search == null);
            }
            else if (option == "Status")
            {
                records = records.Where(x => x.status == search || search == null);
            }

            switch (sort)
            {
                case "descending id":
                    records = records.OrderByDescending(x => x.id_wypozyczenia);
                    break;

                case "descending zamowienia":
                    records = records.OrderByDescending(x => x.data_zamowienia);
                    break;

                case "Data Zamówienia":
                    records = records.OrderBy(x => x.data_zamowienia);
                    break;

                case "descending wypozyczenia":
                    records = records.OrderByDescending(x => x.data_wypozyczenia);
                    break;

                case "Data Wypożyczenia":
                    records = records.OrderBy(x => x.data_wypozyczenia);
                    break;

                case "descending zwrotu":
                    records = records.OrderByDescending(x => x.data_zwrotu);
                    break;

                case "Data Zwrotu":
                    records = records.OrderBy(x => x.data_zwrotu);
                    break;

                case "descending status":
                    records = records.OrderByDescending(x => x.status);
                    break;

                case "Status":
                    records = records.OrderBy(x => x.status);
                    break;
                case "descending ksiazka":
                    records = records.OrderByDescending(x => x.ksiazki.tytul);
                    break;

                case "Książka":
                    records = records.OrderBy(x => x.ksiazki.tytul);
                    break;
                case "descending czytelnik":
                    records = records.OrderByDescending(x => x.czytelnicy.nazwisko).ThenBy(x => x.czytelnicy.imie);
                    break;

                case "Czytelnik":
                    records = records.OrderBy(x => x.czytelnicy.nazwisko).ThenBy(x=>x.czytelnicy.imie);
                    break;
                default:
                    records = records.OrderBy(x => x.id_wypozyczenia);
                    break;

            }
            return View(records.ToPagedList(page ?? 1, 10));

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
                 new SelectListItem{ Value="Zarezerwowane",Text="Zarezerwowane"},
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
                var addBook = db.czytelnicy.Where(x => x.id_czytelnika == wypozyczenia.id_czytelnika).FirstOrDefault();
                addBook.liczba_ksiazek = db.wypozyczenia.Where(x => x.id_czytelnika == addBook.id_czytelnika).Where(x=> x.status == "Wypozyczone").Select(x => x.id_wypozyczenia).Count();
                db.Entry(addBook).State = EntityState.Modified;
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
                 new SelectListItem{ Value="Zarezerwowane",Text="Zarezerwowane"},
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
