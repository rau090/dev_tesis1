using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System.Web;
using wepp_app_v0.Context;

namespace wepp_app_v0.Models
{
    public class AsignacionRecursos
    {
        private struct Fecha
        {
            public string tipo { get; set; }
            public DateTime fecha { get; set; }
        }

        public int Resultado { get; set; }
        public DateTime FechaPlanificacion { get; set; }


        public int Planificacion(DateTime planificacion, EFDbContext db)
        {
            this.Resultado = 0;
            //Obtengo lista de Requerimientos y sus coticizaciones por programar
            List<Requerimiento> requerimientos = db.Requerimientos.Include(r => r.Cotizaciones).Where(r => r.Estado == "Registrado").OrderBy(r => r.Prioridad).ToList();

            if (requerimientos.Count > 0)
            {
                foreach (Requerimiento r in requerimientos)
                {
                    //// ***********************************************************************************
                    //// Buscar Mejor LP e IDS

                    PersonalInterno ids = MejorIdS(db);
                    PersonalInterno lp = MejorLP(db);
                    PlanificarRequerimiento(db, ids, lp, r, planificacion);

                }
                this.Resultado = 1;
            }
            else
            {
                this.Resultado = 0;
            }

            return this.Resultado;
        }


        private void PlanificarRequerimiento(EFDbContext db, PersonalInterno ids, PersonalInterno lp, Requerimiento requerimientoPlanificado, DateTime planificacion)
        {
            
            //*****************************************************************
            //Generar Cronograma

            //Establecer Fecha de Inicio del Proyecto

            List<Fecha> Fechas = new List<Fecha>();

            Fecha fecha_ids = new Fecha();
            if (ids.Actividades.Count > 0)
            {
                fecha_ids.fecha = ids.Actividades[0].FechaFin;
                fecha_ids.tipo = "2ids";
                Fechas.Add(fecha_ids);
            }


            Fecha fecha_lp = new Fecha();
            if (lp.Actividades.Count > 0)
            {
                fecha_lp.fecha = lp.Actividades[0].FechaFin;
                fecha_lp.tipo = "3lp";
                Fechas.Add(fecha_lp);
            }

            Fecha fecha_planificacion = new Fecha();
            fecha_planificacion.fecha = planificacion;
            fecha_planificacion.tipo = "1fecha_planificacion";
            Fechas.Add(fecha_planificacion);

            Fechas.Sort(delegate(Fecha f1, Fecha f2)
            {

                return -DateTime.Compare(f1.fecha, f2.fecha) == 0 ? String.Compare(f1.tipo, f2.tipo) : -DateTime.Compare(f1.fecha, f2.fecha);

            });


            DateTime fechainicio;

            //**************************
            //Fecha Incio
            fechainicio = Fechas[0].fecha;

            //Se genera Cronograma

            Cronograma cronogramaPlanificado = new Cronograma();
            cronogramaPlanificado.IdRequerimiento = requerimientoPlanificado.IdRequerimiento;
            cronogramaPlanificado.NumControlCambio = 0;
            cronogramaPlanificado.FechaInicio = fechainicio;
            cronogramaPlanificado.FechaFin = fechainicio;
            cronogramaPlanificado.Version = 1;
            cronogramaPlanificado.Estado = "Inicial";
            db.Cronogramas.Add(cronogramaPlanificado);
            db.SaveChanges();
            int id = cronogramaPlanificado.IdCronograma;


            //Se generan actividades
            //Analisis funcional

            Actividad analisisFuncional = new Actividad();
            analisisFuncional.IdCronograma = cronogramaPlanificado.IdCronograma;
            analisisFuncional.IdPersonalInterno = ids.IdPersonalInterno;
            analisisFuncional.personalInterno = ids;
            analisisFuncional.OrdenActividad = 1;
            analisisFuncional.TipoActividad = "Analisis Funcional";
            analisisFuncional.FechaInicio = fechainicio;
            analisisFuncional.FechaFin = fechainicio.AddDays(requerimientoPlanificado.Cotizaciones[0].DiasAnalisisFuncional);
            analisisFuncional.Avance = 0;
            analisisFuncional.Asignacion = 1;

            //Analisis tecnico
            Actividad analisisTecnico = new Actividad();
            analisisTecnico.IdCronograma = cronogramaPlanificado.IdCronograma;
            analisisTecnico.IdPersonalInterno = ids.IdPersonalInterno;
            analisisTecnico.personalInterno = ids;
            analisisTecnico.OrdenActividad = 2;
            analisisTecnico.TipoActividad = "Analisis Tecnico";
            analisisTecnico.FechaInicio = analisisFuncional.FechaFin;
            analisisTecnico.FechaFin = analisisTecnico.FechaInicio.AddDays(requerimientoPlanificado.Cotizaciones[0].DiasAnalisisTecnico);
            analisisTecnico.Avance = 0;
            analisisTecnico.Asignacion = 1;


            //Construccion
            Actividad construccion = new Actividad();
            construccion.IdCronograma = cronogramaPlanificado.IdCronograma;
            //construccion.IdPersonalInterno = null;
            //construccion.personalInterno = null;
            construccion.OrdenActividad = 3;
            construccion.TipoActividad = "Construccion";
            construccion.FechaInicio = analisisTecnico.FechaFin;
            construccion.FechaFin = construccion.FechaInicio.AddDays(requerimientoPlanificado.Cotizaciones[0].DiasConstruccion);
            construccion.Avance = 0;
            construccion.Asignacion = 1;

            //Certificacion
            Actividad certificacion = new Actividad();
            certificacion.IdCronograma = cronogramaPlanificado.IdCronograma;
            //certificacion.IdPersonalInterno = null;
            //certificacion.personalInterno = null;
            certificacion.OrdenActividad = 4;
            certificacion.TipoActividad = "Certificacion";
            certificacion.FechaInicio = construccion.FechaFin;
            certificacion.FechaFin = certificacion.FechaInicio.AddDays(requerimientoPlanificado.Cotizaciones[0].DiasCertificacion);
            certificacion.Avance = 0;
            certificacion.Asignacion = 1;

            //Seguimiento
            Actividad seguimiento = new Actividad();
            seguimiento.IdCronograma = cronogramaPlanificado.IdCronograma;
            //seguimiento.IdPersonalInterno = null;
            //seguimiento.personalInterno = null;
            seguimiento.OrdenActividad = 5;
            seguimiento.TipoActividad = "Seguimiento";
            seguimiento.FechaInicio = certificacion.FechaFin;
            seguimiento.FechaFin = seguimiento.FechaInicio.AddDays(requerimientoPlanificado.Cotizaciones[0].DiasSeguimiento);
            seguimiento.Avance = 0;
            seguimiento.Asignacion = 1;


            //gestion
            Actividad gestion = new Actividad();
            gestion.IdCronograma = cronogramaPlanificado.IdCronograma;
            gestion.IdPersonalInterno = lp.IdPersonalInterno;
            gestion.personalInterno = lp;
            gestion.OrdenActividad = 6;
            gestion.TipoActividad = "Gestion";
            gestion.FechaInicio = fechainicio;
            gestion.FechaFin = seguimiento.FechaFin;
            gestion.Avance = 0;
            gestion.Asignacion = 0.15m;

            //grabando actividades
            db.Actividades.Add(analisisFuncional);
            db.Actividades.Add(analisisTecnico);
            db.Actividades.Add(construccion);
            db.Actividades.Add(certificacion);
            db.Actividades.Add(seguimiento);
            db.Actividades.Add(gestion);
            db.SaveChanges();

            //Se actualiza cronograma

            cronogramaPlanificado.FechaInicio = gestion.FechaInicio;
            cronogramaPlanificado.FechaFin = gestion.FechaFin;
            db.Entry(cronogramaPlanificado).State = EntityState.Modified;
            db.SaveChanges();

            //Se asocian IDS y LP e Actualiza Requerimiento
            requerimientoPlanificado.IdS = ids;
            requerimientoPlanificado.IdIdS = ids.IdPersonalInterno;
            requerimientoPlanificado.LiderProyecto = lp;
            requerimientoPlanificado.IdLiderProyecto = lp.IdPersonalInterno;
            requerimientoPlanificado.Estado = "Programado";
            db.Entry(requerimientoPlanificado).State = EntityState.Modified;
            db.SaveChanges();


        }

        private PersonalInterno MejorIdS(EFDbContext db)
        {

            List<PersonalInterno> IdSs = db.PersonalesInternos.Include(p => p.Actividades).Where(p => p.Rol == "IDS").ToList();
            // ***********************************************************************************
            // Pruebo buscar IDS LIBRE
            //Genero lista de IdS Libres

            List<PersonalInterno> IdSLibres = new List<PersonalInterno>();

            foreach (PersonalInterno r in IdSs)
            {
                if (r.Actividades.Count() == 0)
                {
                    //Console.WriteLine("ids - libre " + r.IdPersonalInterno);
                    IdSLibres.Add(r);
                }
            }

            if (IdSLibres.Count() > 0)
            {
                //Se encontró el mejor IdS
                //Console.WriteLine("ids Libre " + IdSLibres[0].Nombre);
                //IdS = IdSLibres[0];
                //IdS.Actividades = IdSLibres[0].Actividades;
                //Console.WriteLine("ids Selecto libre " + IdSLibres[0].IdPersonalInterno);
                //Console.WriteLine("ids Selecto libre " + IdSLibres[0].IdPersonalInterno);
                return IdSLibres[0];
            }

            // ***********************************************************************************
            // En caso no se encontró
            // Se busca el IDS que tiene la actividad tipo AT con fecha fin más próxima

            foreach (PersonalInterno r in IdSs)
            {
                r.Actividades.Sort(delegate(Actividad a1, Actividad a2) { return -DateTime.Compare(a1.FechaFin, a2.FechaFin); });
                //Console.WriteLine("ids Actividad "+r.Nombre+" "+r.Actividades[0].FechaFin + r.Actividades[0].TipoActividad);
            }

            IdSs.Sort(delegate(PersonalInterno ids1, PersonalInterno ids2) { return DateTime.Compare(ids1.Actividades[0].FechaFin, ids2.Actividades[0].FechaFin); });

            //Console.WriteLine("ids Selecto " + IdSs[0].IdPersonalInterno);


            //IdS = IdSs[0];
            //IdS.Actividades = IdSs[0].Actividades;
            //Console.WriteLine("ids Selecto " + IdSs[0].Nombre + " " + IdSs[0].Actividades[0].FechaFin);
            //Console.WriteLine("ids Selecto " + IdS.Nombre + " " + IdS.Actividades[0].FechaFin);
            return IdSs[0];

        }

        private PersonalInterno MejorLP(EFDbContext db)
        {
            List<PersonalInterno> LPs = db.PersonalesInternos.Include(p => p.Actividades).Where(p => p.Rol == "LP").ToList();

            // Pruebo buscar LP LIBRE

            //Genero lista de LP LIBREs


            List<PersonalInterno> LPLibres = new List<PersonalInterno>();

            foreach (PersonalInterno r in LPs)
            {
                if (r.Actividades.Count() == 0)
                {
                    LPLibres.Add(r);
                }
            }

            if (LPLibres.Count() > 0)
            {
                //Se encontró el mejor LP
                //Console.WriteLine("lp Libre " + LPLibres[0].Nombre);

                return LPLibres[0];
            }



            // ***********************************************************************************
            // En caso no se encontró
            // Se busca el LP que tiene la actividad tipo AT con fecha fin más próxima
            foreach (PersonalInterno r in LPs)
            {
                r.Actividades.Sort(delegate(Actividad a1, Actividad a2) { return -DateTime.Compare(a1.FechaFin, a2.FechaFin); });
                //Console.WriteLine("ids Actividad "+r.Nombre+" "+r.Actividades[0].FechaFin + r.Actividades[0].TipoActividad);
            }

            LPs.Sort(delegate(PersonalInterno ids1, PersonalInterno ids2) { return DateTime.Compare(ids1.Actividades[0].FechaFin, ids2.Actividades[0].FechaFin); });

            //Console.WriteLine("ids Selecto " + IdSs[0].IdPersonalInterno);
            //Se encontró LP

            return LPs[0];

        }
    }
}