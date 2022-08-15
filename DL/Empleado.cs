using System;
using System.Collections.Generic;

namespace DL
{
    public partial class Empleado
    {
        public int IdEmpleado { get; set; }
        public string? NumeroEmpleado { get; set; }
        public string? Rfc { get; set; }
        public string Nombre { get; set; } = null!;
        public string ApellidoPaterno { get; set; } = null!;
        public string? ApellidoMaterno { get; set; }
        public string Email { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Nss { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? Foto { get; set; }
        public int? IdEmpresa { get; set; }
        public int? IdPoliza { get; set; }

        public virtual Empresa? IdEmpresaNavigation { get; set; }
        public virtual Poliza? IdPolizaNavigation { get; set; }
        public string EmpresaNombre { get; set; }
        public string PolizaNombre { get; set; }
        //public string Genero { get; set; }
        //public string EstadoCivil { get; set; }
        //public int? IdDependiente { get; set; }
        //public int? IdDependienteTipo { get; set; }
        //public string DependienteNombre { get; set; }



    }
}
