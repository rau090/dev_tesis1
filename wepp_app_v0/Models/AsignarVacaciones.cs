using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Web;
using wepp_app_v0.Context;

namespace wepp_app_v0.Models
{
    public class AsignarVacaciones
    {
        public Vacacion validarPeriodoVacaciones(Vacacion vacacionPropuesta, EFDbContext db)
        {
            List<Actividad> actividades = db.Actividades.Where(a => a.IdPersonalInterno == vacacionPropuesta.IdPersonalInterno).OrderByDescending(a=>a.FechaFin).ToList();
            if (DateTime.Compare(vacacionPropuesta.FechaInicio, actividades[0].FechaFin) > 0)
            {
                return vacacionPropuesta;
            }
            else
            {

            }
            return null;
        }
    }
}