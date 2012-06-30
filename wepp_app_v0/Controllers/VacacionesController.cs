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
    public class VacacionesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /Vacaciones/

        public ViewResult Index()
        {
            var vacaciones = db.Vacaciones.Include(v => v.personalInterno);
            return View(vacaciones.ToList());
        }

        //
        // GET: /Vacaciones/Details/5

        public ViewResult Details(int id)
        {
            Vacacion vacacion = db.Vacaciones.Find(id);
            return View(vacacion);
        }

        //
        // GET: /Vacaciones/Create

        public ActionResult Create()
        {
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno");
            Vacacion vacacion = new Vacacion();
            vacacion.FechaInicio = DateTime.Today;
            return View(vacacion);
        }

        //
        // POST: /Vacaciones/Create

        [HttpPost]
        public ActionResult Create(Vacacion vacacion)
        {
            List<Actividad> actividades = db.Actividades.Where(a => a.IdPersonalInterno == vacacion.IdPersonalInterno).OrderByDescending(a=>a.FechaFin).ToList();
            if (DateTime.Compare(vacacion.FechaInicio,actividades[0].FechaFin)<=0) 
            {
                string error = "La fecha mínima de salida es: " + actividades[0].FechaFin.AddDays(1).ToShortDateString();
                ModelState.AddModelError("FechaInicio", error);
            }
            if (vacacion.Periodo > 20)
            {
                string error = "El periodo máximo de vacaciones es 20 días";
                ModelState.AddModelError("Periodo", error);
            }
            if (vacacion.Periodo <= 5)
            {
                string error = "El periodo mínimo de vacaciones es 5 días";
                ModelState.AddModelError("Periodo", error);
            }
            if (ModelState.IsValid)
            {
                vacacion.FechaFin = vacacion.FechaInicio.AddDays(vacacion.Periodo);
                vacacion.Estado = "Aprobado";
                vacacion.Anho = vacacion.FechaFin.Year;

                db.Vacaciones.Add(vacacion);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", vacacion.IdPersonalInterno);
            return View(vacacion);
        }

        //
        // GET: /Vacaciones/Edit/5

        public ActionResult Edit(int id)
        {
            Vacacion vacacion = db.Vacaciones.Find(id);
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", vacacion.IdPersonalInterno);
            return View(vacacion);
        }

        //
        // POST: /Vacaciones/Edit/5

        [HttpPost]
        public ActionResult Edit(Vacacion vacacion)
        {
            if (vacacion.Periodo > 20)
            {
                string error = "El periodo máximo de vacaciones es 20 días";
                ModelState.AddModelError("Periodo", error);
            }
            if (vacacion.Periodo <= 5)
            {
                string error = "El periodo mínimo de vacaciones es 5 días";
                ModelState.AddModelError("Periodo", error);
            }
            if (ModelState.IsValid)
            {
                vacacion.FechaFin = vacacion.FechaInicio.AddDays(vacacion.Periodo);
                vacacion.Estado = "Aprobado";
                vacacion.Anho = vacacion.FechaFin.Year;
                db.Entry(vacacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdPersonalInterno = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoMaterno", vacacion.IdPersonalInterno);
            return View(vacacion);
        }

        //
        // GET: /Vacaciones/Delete/5

        public ActionResult Delete(int id)
        {
            Vacacion vacacion = db.Vacaciones.Find(id);
            return View(vacacion);
        }

        //
        // POST: /Vacaciones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Vacacion vacacion = db.Vacaciones.Find(id);
            db.Vacaciones.Remove(vacacion);
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