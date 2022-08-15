using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Dependiente
    {
        public int IdDependiente { get; set; }
        public string? Nombre { get; set; }
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? EstadoCivil { get; set; }
        public string? Genero { get; set; }
        public string? Telefono { get; set; }
        public string? Rfc { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdDependienteTipo { get; set; }
        //public string? EmpleadoNombre { get; set; }
        public string? TipoDependiente { get; set; }

        public virtual DependienteTipo? IdDependienteTipoNavigation { get; set; }
        public virtual Empleado? IdEmpleadoNavigation { get; set; }
    }
}
