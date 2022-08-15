using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Poliza
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Polizas.FromSqlRaw($"PolizaNombreGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Poliza poliza = new ML.Poliza();
                            poliza.IdPoliza = obj.IdPoliza;
                            poliza.Nombre = obj.Nombre;

                            result.Objects.Add(poliza);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro registro de la poliza";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }
            return result;
        }
    }
}
