using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace wepp_app_v0.Models
{
    public class PersonalInterno
    {
        [Key]
        public int IdPersonalInterno { get; set; }
        
        public string ApellidoMaterno { get; set; }
        public string ApellidoPaterno { get; set; }
        public string EstadoPersonal { get; set; }
        public string Nombre { get; set; }
        public string Rol { get; set; }
        public virtual List<Requerimiento> Requerimientos { get; set; }
        public virtual List<Actividad> Actividades { get; set; }
        public virtual List<Vacacion> Vacaciones { get; set; }


    }
}