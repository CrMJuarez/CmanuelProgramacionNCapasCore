using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Aseguradora
    {
        public static ML.Result AddEF(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}");
                   
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo agregar la aseguradora";
                    }

                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }

        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var aseguradoras = context.Aseguradoras.FromSqlRaw($"AseguradoraGetAll").ToList();
                   
                    result.Objects = new List<object>();

                    if (aseguradoras != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in aseguradoras)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();

                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModificacion.ToString();
                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = obj.IdUsuario.Value;
                            aseguradora.Usuario.Nombre = obj.UsuarioNombre;


                            result.Objects.Add(aseguradora);
                        }
                        result.Correct = true;
                    }
                    else

                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de mostrar el usuario";
                    }

                }

            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }

        public static ML.Result GetByIdEF(int IdAseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var objAseguradora = context.Aseguradoras.FromSqlRaw($"AseguradoraGetById {IdAseguradora}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (objAseguradora != null)
                    {

                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = objAseguradora.IdAseguradora;
                        aseguradora.Nombre = objAseguradora.Nombre;
                        aseguradora.FechaCreacion = objAseguradora.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = objAseguradora.FechaModificacion.ToString();
                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = int.Parse(objAseguradora.IdUsuario.ToString());

                        result.Object = aseguradora;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de mostrar la aseguradora";
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }

        public static ML.Result DeleteEF(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query =context.Database.ExecuteSqlRaw($"AseguradoraDelete {aseguradora.IdAseguradora}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la aseguradora";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;

        }

        public static ML.Result UpdateEF(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var updateResult = context.Database.ExecuteSqlRaw($"AseguradoraUpdate '{aseguradora.Nombre}',{aseguradora.IdAseguradora}, {aseguradora.Usuario.IdUsuario}");


                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo modificar la aseguradora";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;

        }
    }
}
