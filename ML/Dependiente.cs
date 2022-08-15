using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dependiente
    {
        public int IdDependiente { get; set; }
        public string Nombre { get; set; }
        public string ApeliidoPaterno { get; set; }
        public string AppellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public string EstadoCivil { get; set; }
        public string Genero { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public int IdEmpleado { get; set; }
        public int IdDependienteTipo { get; set; }
        public ML.DependienteTipo? dependienteTipo { get; set; }
        public ML.Empleado? empleado { get; set; }
        public List<object> Dependientes { get; set; }
    }
}

