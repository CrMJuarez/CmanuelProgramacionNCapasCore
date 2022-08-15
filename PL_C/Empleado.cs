using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_C
{
    public class Empleado
    {
        public static ML.Result CargaMasiva()
        {
            ML.Result result = new ML.Result();
            string file = @"C:\Users\ALIEN09\Documents\Cmanuel\LayoutEmpleado.txt";

            StreamReader archivo = new StreamReader(file);

            string line;
            bool isFirstLine = true;
            while ((line = archivo.ReadLine()) != null)
            {
                if (isFirstLine)
                {
                    isFirstLine = false;
                    line = archivo.ReadLine();
                }

                Console.WriteLine(line);
                string[] datos = line.Split('|');

                ML.Empleado empleado = new ML.Empleado();
                empleado.NumeroEmpleado = datos[0];
                empleado.Rfc = datos[1];
                empleado.Nombre = datos[2];
                empleado.ApellidoPaterno = datos[3];
                empleado.ApellidoMaterno = datos[4];
                empleado.Email = datos[5];
                empleado.Telefono = datos[6];
                empleado.FechaNacimiento = datos[7].ToString();
                empleado.Nss = datos[8];
                empleado.FechaIngreso = datos[9].ToString();
                empleado.Foto = null;
                empleado.Empresa = new ML.Empresa();
                empleado.Empresa.IdEmpresa = int.Parse(datos[11]);
                empleado.Poliza = new ML.Poliza();
                empleado.Poliza.IdPoliza = int.Parse(datos[12]);

                result = BL.Empleado.AddEF(empleado);

            }

            return result;
        }
    }
}
