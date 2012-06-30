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
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:P}")]
        [Range(0, 1, ErrorMessage = "El valor de {0} debe ser entre {1} y {2}")]
        public decimal Avance { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:P}")]
        [Range(0, 1, ErrorMessage = "El valor de {0} debe ser entre {1} y {2}")]
        public decimal Asignacion { get; set; }


    }
}