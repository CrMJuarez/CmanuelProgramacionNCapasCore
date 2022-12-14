using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Poliza
    {
        public int IdPoliza { get; set; }
        public string? Nombre { get; set; }
        public string? NombrePoliza { get; set; }

        public string NumeroPoliza { get; set; }
        public string FechaCreacion { get; set; }
        public string FechaModificacion { get; set; }

        // agregar las llaves o ml 
        public ML.Usuario Usuario { get; set; }
        public ML.SubPoliza SubPoliza { get; set; }
        public List<object> Polizas { get; set; }

    }
}
