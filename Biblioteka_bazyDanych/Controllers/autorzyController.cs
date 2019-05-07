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
using System.Data.SqlClient;

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

        public ActionResult Index(string option, string search, int? page, string sort)
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

            //if the sort parameter is null or empty then we are initializing the value as descending name  
            ViewBag.SortByID = string.IsNullOrEmpty(sort) ? "descending id" : "";
            ViewBag.SortByName = sort == "Imię" ? "descending imie" : "Imię";
            //if the sort value is gender then we are initializing the value as descending gender  
            ViewBag.SortBySurname = sort == "Nazwisko" ? "descending nazwisko" : "Nazwisko";
            ViewBag.SortByNationality = sort == "Narodowość" ? "descending narodowosc" : "Narodowość";
            ViewBag.SortByBooks = sort == "Liczba Dzieł" ? "descending dziela" : "Liczba Dzieł";

            //here we are converting the db.autorzy to AsQueryable so that we can invoke all the extension methods on variable records.  
            var records = db.autorzy.AsQueryable();

            if (option == "Imię")
            {
                records = records.Where(x => x.imie == search || search == null);
            }
            else if (option == "Nazwisko")
            {
                records = records.Where(x => x.nazwisko == search || search == null);
            }
            else if (option == "Narodowość")
            {
                records = records.Where(x => x.narodowosc == search || search == null);
            }

            switch (sort)
            {
                case "descending id":
                    records = records.OrderByDescending(x => x.id_autora);
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

                case "descending narodowosc":
                    records = records.OrderByDescending(x => x.narodowosc);
                    break;

                case "Narodowość":
                    records = records.OrderBy(x => x.narodowosc);
                    break;

                case "descending dziela":
                    records = records.OrderByDescending(x => x.liczba_dziel);
                    break;

                case "Liczba Dzieł":
                    records = records.OrderBy(x => x.liczba_dziel);
                    break;
                default:
                    records = records.OrderBy(x => x.id_autora);
                    break;

            }
            return View(records.ToPagedList(page ?? 1, 10));
        

            /*  OLD VERSION WITHOUT SORTING(JUST SEARCHING)
        
            //if a user choose the radio button option as Imię  
            if (option == "Imię")
            {
                //Index action method will return a view with a student records based on what a user specify the value in textbox  
                return View(db.autorzy.Where(x => x.imie == search || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else if (option == "Nazwisko")
            {
                return View(db.autorzy.Where(x => x.nazwisko == search || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else if (option == "Narodowość")
            {
                return View(db.autorzy.Where(x => x.narodowosc == search || search == null).ToList().ToPagedList(page ?? 1, 5));
            }
            else
            {
                return View(db.autorzy.ToList().ToPagedList(page ?? 1, 5));
            }


            */
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
            try
            {
                autorzy autorzy = db.autorzy.Find(id);
                db.autorzy.Remove(autorzy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                return View("DeleteError");
            }
            catch (Exception ex)
            {
                return View("DeleteError");
            }
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
