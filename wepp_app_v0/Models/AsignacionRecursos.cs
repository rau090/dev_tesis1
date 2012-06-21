using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wepp_app_v0.Context;

namespace wepp_app_v0.Models
{
    public class AsignacionRecursos
    {
        public static PersonalInterno mejorIdS()
        {
            EFDbContext bd = new EFDbContext();
            List<PersonalInterno> IdSs = bd.PersonalesInternos.Include(p => p.Actividades).Where(p => p.Rol == "IDS").ToList();
            // ***********************************************************************************
            // Pruebo buscar IDS LIBRE
            //Genero lista de IdS Libres

            List<PersonalInterno> IdSLibres = new List<PersonalInterno>();

            foreach (PersonalInterno r in IdSs)
            {
                if (r.Actividades.Count() == 0)
                {
                    IdSLibres.Add(r);
                }
            }

            if (IdSLibres.Count() > 0)
            {
                //Se encontró el mejor IdS
                //Console.WriteLine("ids Libre " + IdSLibres[0].Nombre);
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
            //Se encontró LP
            return IdSs[0];

        }

        public static PersonalInterno mejorLP()
        {
            EFDbContext bd = new EFDbContext();
            List<PersonalInterno> LPs = bd.PersonalesInternos.Include(p => p.Actividades).Where(p => p.Rol == "LP").ToList();

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
                Console.WriteLine("lp Libre " + LPLibres[0].Nombre);
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