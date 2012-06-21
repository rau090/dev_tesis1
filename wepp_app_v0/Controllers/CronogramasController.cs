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
    public class CronogramasController : Controller
    {
        private EFDbContext db = new EFDbContext();

        //
        // GET: /Cronogramas/

        public ViewResult Index()
        {
            var cronogramas = db.Cronogramas.Include(c => c.requerimiento);
            return View(cronogramas.ToList());
        }

        //
        // GET: /Cronogramas/Details/5

        public ViewResult Details(int id)
        {
            Cronograma cronograma = db.Cronogramas.Find(id);
            return View(cronograma);
        }

        //
        // GET: /Cronogramas/Create

        public ActionResult Create()
        {
            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq");
            return View();
        } 

        //
        // POST: /Cronogramas/Create

        [HttpPost]
        public ActionResult Create(Cronograma cronograma)
        {
            if (ModelState.IsValid)
            {
                db.Cronogramas.Add(cronograma);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq", cronograma.IdRequerimiento);
            return View(cronograma);
        }
        
        //
        // GET: /Cronogramas/Edit/5
 
        public ActionResult Edit(int id)
        {
            Cronograma cronograma = db.Cronogramas.Find(id);
            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq", cronograma.IdRequerimiento);
            return View(cronograma);
        }

        //
        // POST: /Cronogramas/Edit/5

        [HttpPost]
        public ActionResult Edit(Cronograma cronograma)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cronograma).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRequerimiento = new SelectList(db.Requerimientos, "IdRequerimiento", "NumReq", cronograma.IdRequerimiento);
            return View(cronograma);
        }

        //
        // GET: /Cronogramas/Delete/5
 
        public ActionResult Delete(int id)
        {
            Cronograma cronograma = db.Cronogramas.Find(id);
            return View(cronograma);
        }

        //
        // POST: /Cronogramas/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            Cronograma cronograma = db.Cronogramas.Find(id);
            db.Cronogramas.Remove(cronograma);
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