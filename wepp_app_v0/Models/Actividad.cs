using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wepp_app_v0.Models
{
    public class Actividad
    {
        [Key]
        public int IdActividad { get; set; }

        public int IdCronograma { get; set; }
        public virtual Cronograma cronograma { get; set; }

        public Nullable<int> IdPersonalInterno { get; set; }
        public virtual PersonalInterno personalInterno { get; set; }

        public int OrdenActividad { get; set; }
        public string TipoActividad { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime FechaFin { get; set; }
        public decimal Avance { get; set; }
        public decimal Asignacion { get; set; }


    }
}