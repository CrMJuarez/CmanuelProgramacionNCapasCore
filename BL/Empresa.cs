using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAllEF()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var empresas = context.Empresas.FromSqlRaw($"EmpresaGetAll").ToList();

                    result.Objects = new List<object>();

                    if (empresas != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in empresas)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;
                            empresa.Telefono = obj.Telefono;
                            empresa.Email = obj.Email;
                            empresa.DireccionWeb = obj.DireccionWeb;
                            empresa.Logo = obj.Logo;
                            


                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;
                    }
                    else

                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de mostrar la empresa";
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
        public static ML.Result GetByIdEF(int IdEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var objEmpresa = context.Empresas.FromSqlRaw($"EmpresaGetById {IdEmpresa}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (objEmpresa != null)
                    {

                        ML.Empresa empresa = new ML.Empresa();

                        empresa.IdEmpresa = objEmpresa.IdEmpresa;
                        empresa.Nombre = objEmpresa.Nombre;
                        empresa.Telefono = objEmpresa.Telefono;
                        empresa.Email = objEmpresa.Email;
                        empresa.DireccionWeb = objEmpresa.DireccionWeb;
                        empresa.Logo = objEmpresa.Logo;

                        result.Object = empresa;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de mostrar la empresa";
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

        public static ML.Result DeleteEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaDelete {empresa.IdEmpresa}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo eliminar la empresa";
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
        public static ML.Result AddEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpresaAdd '{empresa.Nombre}', '{empresa.Telefono}','{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo agregar la empresa";
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

        public static ML.Result UpdateEF(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var updateResult = context.Database.ExecuteSqlRaw($"EmpresaUpdate '{empresa.Nombre}', '{empresa.Telefono}','{empresa.Email}','{empresa.DireccionWeb}','{empresa.Logo}',{empresa.IdEmpresa}");


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

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Empresas.FromSqlRaw($"EmpresaNombreGetAll").ToList();
                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();
                            empresa.IdEmpresa = obj.IdEmpresa;
                            empresa.Nombre = obj.Nombre;

                            result.Objects.Add(empresa);
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
