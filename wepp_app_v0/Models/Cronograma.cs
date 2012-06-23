using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wepp_app_v0.Models
{
    public class Cronograma
    {
        [Key]
        public int IdCronograma { get; set; }

        public virtual Requerimiento requerimiento { get; set; }
        public int IdRequerimiento { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FechaFin { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FechaInicio { get; set; }
        public int NumControlCambio { get; set; }
        public int Version { get; set; }
        public string Estado { get; set; }

        public virtual List<Actividad> Actividades { get; set; }


    }
}