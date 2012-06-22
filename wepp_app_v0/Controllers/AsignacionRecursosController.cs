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
    public class AsignacionRecursosController : Controller
    {
        private EFDbContext db = new EFDbContext();
        //
        // GET: /AsignarcionRecursos/

        public ViewResult Index()
        {
            return View();
        }





        public ActionResult Create()
        {
            List<Requerimiento> reqs = db.Requerimientos.Include(r => r.LiderProyecto).Include(r => r.IdS).Where(r=>r.Estado=="Programado").OrderBy(r=>r.Prioridad).ToList();
            ViewBag.Requerimientos = reqs;
            return View();
        }

        //
        // POST: /AsignarcionRecursos/
        [HttpPost]
        public ActionResult Create(AsignacionRecursos asignacionrecursos)
        {

            if (ModelState.IsValid)
            {
                asignacionrecursos.Planificacion(asignacionrecursos.FechaPlanificacion, db);
   
                return RedirectToAction("Index");
            }

            return View(asignacionrecursos);
        }

        

    }
}
