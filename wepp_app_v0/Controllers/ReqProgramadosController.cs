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
    public class ReqProgramadosController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /ReqProgramados/

        public ViewResult Index()
        {
            var requerimientos = db.Requerimientos.Include(r => r.LiderProyecto).Include(r => r.IdS).Include(r=>r.Cronogramas).Where(r=>r.Cronogramas.Count>0);
            return View(requerimientos.ToList());
        }

        //
        // GET: /ReqProgramados/Details/5

        public ViewResult Details(int id)
        {
            Requerimiento requerimiento = db.Requerimientos.Find(id);
            return View(requerimiento);
        }

        //
        // GET: /ReqProgramados/Create

        public ActionResult Create()
        {
            ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno");
            ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno");
            return View();
        } 

        //
        // POST: /ReqProgramados/Create

        [HttpPost]
        public ActionResult Create(Requerimiento requerimiento)
        {
            if (ModelState.IsValid)
            {
                db.Requerimientos.Add(requerimiento);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", requerimiento.IdLiderProyecto);
            ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", requerimiento.IdIdS);
            return View(requerimiento);
        }
        
        ////
        //// GET: /ReqProgramados/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    Requerimiento requerimiento = db.Requerimientos.Find(id);
        //    ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", requerimiento.IdLiderProyecto);
        //    ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", requerimiento.IdIdS);
        //    return View(requerimiento);
        //}

        ////
        //// POST: /ReqProgramados/Edit/5

        //[HttpPost]
        //public ActionResult Edit(Requerimiento requerimiento)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(requerimiento).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", requerimiento.IdLiderProyecto);
        //    ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", requerimiento.IdIdS);
        //    return View(requerimiento);
        //}

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
        // GET: /ReqProgramados/Delete/5
 
        public ActionResult Delete(int id)
        {
            Requerimiento requerimiento = db.Requerimientos.Find(id);
            return View(requerimiento);
        }

        //
        // POST: /ReqProgramados/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Requerimiento requerimiento = db.Requerimientos.Find(id);
            db.Requerimientos.Remove(requerimiento);
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