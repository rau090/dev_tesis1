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
    public class RequerimientosController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /Requerimientos/

        public ViewResult Index()
        {
            var requerimientos = db.Requerimientos.Include(r => r.LiderProyecto).Include(r => r.IdS).Where(r=>r.Estado=="Registrado");
            return View(requerimientos.ToList());
        }

        //
        // GET: /Requerimientos/Details/5

        public ViewResult Details(int id)
        {
            Requerimiento requerimiento = db.Requerimientos.Find(id);
            return View(requerimiento);
        }

        //
        // GET: /Requerimientos/Create

        public ActionResult Create()
        {
            ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno");
            ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno");
            return View();
        } 

        //
        // POST: /Requerimientos/Create

        [HttpPost]
        public ActionResult Create(Requerimiento requerimiento)
        {
            if (ModelState.IsValid)
            {
                requerimiento.Estado = "Registrado";
                db.Requerimientos.Add(requerimiento);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno", requerimiento.IdLiderProyecto);
            ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno", requerimiento.IdIdS);
            return View(requerimiento);
        }
        
        //
        // GET: /Requerimientos/Edit/5
 
        public ActionResult Edit(int id)
        {
            Requerimiento requerimiento = db.Requerimientos.Find(id);
            ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno", requerimiento.IdLiderProyecto);
            ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno", requerimiento.IdIdS);
            return View(requerimiento);
        }

        //
        // POST: /Requerimientos/Edit/5

        [HttpPost]
        public ActionResult Edit(Requerimiento requerimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requerimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdLiderProyecto = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno", requerimiento.IdLiderProyecto);
            ViewBag.IdIdS = new SelectList(db.PersonalesInternos, "IdPersonalInterno", "ApellidoPaterno", requerimiento.IdIdS);
            return View(requerimiento);
        }

        //
        // GET: /Requerimientos/Delete/5
 
        public ActionResult Delete(int id)
        {
            Requerimiento requerimiento = db.Requerimientos.Find(id);
            return View(requerimiento);
        }

        //
        // POST: /Requerimientos/Delete/5

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