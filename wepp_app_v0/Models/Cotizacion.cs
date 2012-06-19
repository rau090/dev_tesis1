using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace wepp_app_v0.Models
{
    public class Cotizacion
    {
        [Key]
        public int IdCotizacion { get; set; }
        
        public Nullable<int> IdControlCambio { get; set; }
        public string TipoCot { get; set; }
        public int DiasAnalisisFuncional { get; set; }
        public int DiasAnalisisTecnico { get; set; }
        public int DiasConstruccion { get; set; }
        public int DiasCertificacion { get; set; }
        public int DiasSeguimiento { get; set; }
        public int EsfuerzoAnalisisFuncional { get; set; }
        public int EsfuerzoAnalisisTecnico { get; set; }
        public int EsfuerzoConstruccion { get; set; }
        public int EsfuerzoCertificacion { get; set; }
        public int EsfuerzoGestion { get; set; }
        public int EsfuerzoTotal { get; set; }
        public decimal CostoPersonalInterno { get; set; }
        public decimal CostoConstruccion { get; set; }
        public decimal CostoCertificacion { get; set; }
        public decimal CostoTotal { get; set; }


        public int IdRequerimiento { get; set; }
        public virtual Requerimiento requerimiento { get; set; }



    }
}