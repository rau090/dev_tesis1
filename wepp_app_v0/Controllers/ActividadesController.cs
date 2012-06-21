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
    public class ActividadesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /Actividades/

        public ViewResult Index()
        {
            var actividades = db.Actividades.Include(a=>a.cronograma).Include(a => a.personalInterno);
            return View(actividades.ToList());
        }

        //
        // GET: /Actividades/Details/5

        public ViewResult Details(int id)
        {
            Actividad actividad = db.Actividades.Find(id);
            return View(actividad);
        }

        //
        // GET: /Actividades/Create

        public ActionResult Create()
        {
            ViewBag.IdCronograma = new SelectList(db.Cronogramas, "IdCronograma", "Estado");
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno");
            return View();
        } 

        //
        // POST: /Actividades/Create

        [HttpPost]
        public ActionResult Create(Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                db.Actividades.Add(actividad);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdCronograma = new SelectList(db.Cronogramas, "IdCronograma", "Estado", actividad.IdCronograma);
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", actividad.IdPersonalInterno);
            return View(actividad);
        }
        
        //
        // GET: /Actividades/Edit/5
 
        public ActionResult Edit(int id)
        {
            Actividad actividad = db.Actividades.Find(id);
            ViewBag.IdCronograma = new SelectList(db.Cronogramas, "IdCronograma", "Estado", actividad.IdCronograma);
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", actividad.IdPersonalInterno);
            return View(actividad);
        }

        //
        // POST: /Actividades/Edit/5

        [HttpPost]
        public ActionResult Edit(Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actividad).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdCronograma = new SelectList(db.Cronogramas, "IdCronograma", "Estado", actividad.IdCronograma);
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", actividad.IdPersonalInterno);
            return View(actividad);
        }

        //
        // GET: /Actividades/Delete/5
 
        public ActionResult Delete(int id)
        {
            Actividad actividad = db.Actividades.Find(id);
            return View(actividad);
        }

        //
        // POST: /Actividades/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Actividad actividad = db.Actividades.Find(id);
            db.Actividades.Remove(actividad);
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