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
            var requerimientos = db.Requerimientos.Include(r => r.Cotizaciones).Where(r => r.Estado == "Registrado" && r.Cotizaciones.Count>0).OrderBy(r => r.Prioridad);
            return View(requerimientos.ToList());
        }





        public ActionResult Create()
        {
            AsignacionRecursos asignacionrecursos = new AsignacionRecursos();
            asignacionrecursos.FechaPlanificacion = DateTime.Now;
            List<Requerimiento> reqs = db.Requerimientos.Include(r => r.LiderProyecto).Include(r => r.IdS).Where(r=>r.Estado=="Programado").OrderBy(r=>r.Prioridad).ToList();
            ViewBag.Requerimientos = reqs;
            return View(asignacionrecursos);
        }

        //
        // POST: /AsignarcionRecursos/
        [HttpPost]
        public ActionResult Create(AsignacionRecursos asignacionrecursos)
        {

            if (ModelState.IsValid)
            {
                asignacionrecursos.Planificacion(asignacionrecursos.FechaPlanificacion, db);
                if (asignacionrecursos.Resultado == 1)
                {
                    return RedirectToAction("Index");
                } 
            }

            return View(asignacionrecursos);
        }

        

    }
}
