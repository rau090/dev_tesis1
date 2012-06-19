using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wepp_app_v0.Models;
using wepp_app_v0.Context;

namespace wepp_app_v0.Controllers
{ 
    public class RecursosController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /Recursos/

        public ViewResult Index()
        {
            return View(db.PersonalesInternos.ToList());
        }

        //
        // GET: /Recursos/Details/5

        public ViewResult Details(int id)
        {
            PersonalInterno personalinterno = db.PersonalesInternos.Find(id);
            return View(personalinterno);
        }

        //
        // GET: /Recursos/Create

        public ActionResult Create()
        {
            ViewBag.Roles = new SelectList(new[] 
            {
                new { Value = "LP", Text = "Lider de Proyecto" },
                new { Value = "IDS", Text = "Ingeniero de Soluciones" },
            }, "Value", "Text");
            return View();
        } 

        //
        // POST: /Recursos/Create

        [HttpPost]
        public ActionResult Create(PersonalInterno personalinterno)
        {
            ViewBag.Roles = new SelectList(new[] 
            {
                new { Value = "LP", Text = "Lider de Proyecto" },
                new { Value = "IDS", Text = "Ingeniero de Soluciones" },
            }, "Value", "Text");

            if (ModelState.IsValid)
            {
                db.PersonalesInternos.Add(personalinterno);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(personalinterno);
        }
        
        //
        // GET: /Recursos/Edit/5
 
        public ActionResult Edit(int id)
        {
            PersonalInterno personalinterno = db.PersonalesInternos.Find(id);
            return View(personalinterno);
        }

        //
        // POST: /Recursos/Edit/5

        [HttpPost]
        public ActionResult Edit(PersonalInterno personalinterno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalinterno).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalinterno);
        }

        //
        // GET: /Recursos/Delete/5
 
        public ActionResult Delete(int id)
        {
            PersonalInterno personalinterno = db.PersonalesInternos.Find(id);
            return View(personalinterno);
        }

        //
        // POST: /Recursos/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            PersonalInterno personalinterno = db.PersonalesInternos.Find(id);
            db.PersonalesInternos.Remove(personalinterno);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}