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
    public class CotizacionesController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /Cotizaciones/

        public ViewResult Index()
        {
            var cotizaciones = db.Cotizaciones.Include(c => c.requerimiento);
            return View(cotizaciones.ToList());
        }

        //
        // GET: /Cotizaciones/Details/5

        public ViewResult Details(int id)
        {
            Cotizacion cotizacion = db.Cotizaciones.Find(id);
            return View(cotizacion);
        }

        //
        // GET: /Cotizaciones/Create

        public ActionResult Create()
        {
            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos.Where(r=>r.Estado=="Registrado"), "IdRequerimiento", "NumReq");
            return View();
        } 

        //
        // POST: /Cotizaciones/Create

        [HttpPost]
        public ActionResult Create(Cotizacion cotizacion)
        {
            if (ModelState.IsValid)
            {
                cotizacion.EsfuerzoTotal = cotizacion.EsfuerzoAnalisisFuncional + cotizacion.EsfuerzoAnalisisTecnico + 
                    cotizacion.EsfuerzoCertificacion + cotizacion.EsfuerzoConstruccion + cotizacion.EsfuerzoGestion;
                cotizacion.CostoTotal = cotizacion.CostoConstruccion + cotizacion.CostoPersonalInterno + cotizacion.CostoCertificacion;
                    
                db.Cotizaciones.Add(cotizacion);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq", cotizacion.IdRequerimiento);
            return View(cotizacion);
        }
        
        //
        // GET: /Cotizaciones/Edit/5
 
        public ActionResult Edit(int id)
        {
            Cotizacion cotizacion = db.Cotizaciones.Find(id);
            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq", cotizacion.IdRequerimiento);
            return View(cotizacion);
        }

        //
        // POST: /Cotizaciones/Edit/5

        [HttpPost]
        public ActionResult Edit(Cotizacion cotizacion)
        {
            if (ModelState.IsValid)
            {
                cotizacion.EsfuerzoTotal = cotizacion.EsfuerzoAnalisisFuncional + cotizacion.EsfuerzoAnalisisTecnico +
                    cotizacion.EsfuerzoCertificacion + cotizacion.EsfuerzoConstruccion + cotizacion.EsfuerzoGestion;
                cotizacion.CostoTotal = cotizacion.CostoConstruccion + cotizacion.CostoPersonalInterno + cotizacion.CostoCertificacion;
                db.Entry(cotizacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq", cotizacion.IdRequerimiento);
            return View(cotizacion);
        }

        //
        // GET: /Cotizaciones/Delete/5
 
        public ActionResult Delete(int id)
        {
            Cotizacion cotizacion = db.Cotizaciones.Find(id);
            return View(cotizacion);
        }

        //
        // POST: /Cotizaciones/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Cotizacion cotizacion = db.Cotizaciones.Find(id);
            db.Cotizaciones.Remove(cotizacion);
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