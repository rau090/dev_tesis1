using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace wepp_app_v0.Models
{
    public class Requerimiento
    {
        [Key]
        public int IdRequerimiento { get; set; }
        [Required]
        public int NumReq { get; set; }
        public Nullable<int> IdHojaPriorizacion { get; set; }
        public Nullable<int> IdResultadoAsignacion { get; set; }
        public Nullable<int> IdLiderProyecto { get; set; }
        public Nullable<int> IdIdS { get; set; }
        public virtual PersonalInterno LiderProyecto { get; set; }
        public virtual PersonalInterno IdS { get; set; }
        public int AnhoPriorizacion { get; set; }
        public int PeriodoPriorizacion { get; set; }
        public DateTime FechaPriorizacion { get; set; }
        public string LiderUsuario { get; set; }
        public string NivelComplejidad { get; set; }
        public string UnidadNegocio { get; set; }
        public string Estado { get; set; }
        public int Prioridad { get; set; }

        public virtual List<Cotizacion> Cotizaciones { get; set; }
        public virtual List<Cronograma> Cronogramas { get; set; }
        
        
      
    }
}