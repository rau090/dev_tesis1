using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wepp_app_v0.Models
{
    public class Vacacion
    {
        [Key]
        public int IdVacacion { get; set; }

        
        public int Anho { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio {get;set;}

        public int Periodo { get; set; }

        public int IdPersonalInterno { get; set; }
        public virtual PersonalInterno personalInterno { get; set; }


    }
}