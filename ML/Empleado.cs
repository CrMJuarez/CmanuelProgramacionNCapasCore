using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        public int IdEmpleado { get; set; }
        [Required(ErrorMessage = "Debes insertar numero de empleado")]
        public string NumeroEmpleado { get; set; }
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        [Required(ErrorMessage = "Debes insertar email")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "formato no valido")]
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string FechaNacimiento { get; set; }
        public string Nss { get; set; }
        public string FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public  ML.Empresa Empresa { get; set; }
        public ML.Poliza Poliza { get; set; }
        //public ML.Dependiente Dependiente { get; set; }
        //public ML.DependienteTipo DependienteTipo { get; set; }
        public List<object> Empleados { get; set; }
    }
}