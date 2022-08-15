using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        //public static ML.Result GetAllEF()
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
        //        {
        //            var dependientes = context.Dependientes.FromSqlRaw($"DependienteGetAll").ToList();

        //            result.Objects = new List<object>();

        //            if (dependientes != null)
        //            {
        //                result.Objects = new List<object>();

        //                foreach (var obj in dependientes)
        //                {
        //                    ML.Dependiente dependiente = new ML.Dependiente();

        //                    dependiente.IdDependiente = obj.IdDependiente;
        //                    dependiente.Nombre = obj.Nombre;
        //                    dependiente.ApeliidoPaterno = obj.ApellidoPaterno;
        //                    dependiente.AppellidoMaterno = obj.ApellidoMaterno;
        //                    dependiente.FechaNacimiento = obj.FechaNacimiento.ToString();
        //                    dependiente.EstadoCivil = obj.EstadoCivil;
        //                    dependiente.Genero = obj.Genero;
        //                    dependiente.Telefono = obj.Telefono;
        //                    dependiente.Rfc = obj.Rfc;

        //                    dependiente.empleado = new ML.Empleado();
        //                    dependiente.empleado.IdEmpleado = obj.IdEmpleado.Value;
        //                    dependiente.empleado.Nombre = obj.EmpleadoNombre;

        //                    dependiente.dependienteTipo = new ML.DependienteTipo();
        //                    dependiente.dependienteTipo.IdDependienteTipo = obj.IdDependienteTipo.Value;
        //                    dependiente.dependienteTipo.Nombre = obj.TipoDependiente;

        //                    result.Objects.Add(dependiente);
        //                }
        //                result.Correct = true;
        //            }
        //            else

        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "Ocurrió un error al momento de mostrar el dependiente";
        //            }

        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;
        //}
        //public static ML.Result GetById(int IdDependiente)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
        //        {
        //            var dependientes = context.Dependientes.FromSqlRaw($"DependienteGetById {IdDependiente}").AsEnumerable().FirstOrDefault();
        //            result.Objects = new List<object>();
        //            if (dependientes != null)
        //            {
        //                    ML.Dependiente dependiente = new ML.Dependiente();

        //                    dependiente.IdDependiente = dependientes.IdDependiente;
        //                    dependiente.Nombre = dependientes.Nombre;
        //                    dependiente.ApeliidoPaterno = dependientes.ApellidoPaterno;
        //                    dependiente.AppellidoMaterno = dependientes.ApellidoMaterno;
        //                    dependiente.FechaNacimiento = dependientes.FechaNacimiento.ToString();
        //                    dependiente.EstadoCivil = dependientes.EstadoCivil;
        //                    dependiente.Genero = dependientes.Genero;
        //                    dependiente.Telefono = dependientes.Telefono;
        //                    dependiente.Rfc = dependientes.Rfc;

        //                    dependiente.empleado = new ML.Empleado();
        //                    dependiente.empleado.IdEmpleado = dependientes.IdEmpleado.Value;
        //                    dependiente.empleado.Nombre = dependientes.EmpleadoNombre;

        //                    dependiente.dependienteTipo = new ML.DependienteTipo();
        //                    dependiente.dependienteTipo.IdDependienteTipo = dependientes.IdDependienteTipo.Value;
        //                    dependiente.dependienteTipo.Nombre = dependientes.TipoDependiente;

        //                result.Object = dependiente;
        //                result.Correct = true;
        //            }
        //            else

        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "Ocurrió un error al momento de mostrar el dependiente";
        //            }

        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;
        //}

        //public static ML.Result DeleteEF(ML.Dependiente dependiente)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
        //        {
        //            var query = context.Database.ExecuteSqlRaw($"DependienteDelete {dependiente.IdDependiente}");
        //            if (query >= 1)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se pudo eliminar el dependiente";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;

        //}
        //public static ML.Result AddEF(ML.Dependiente dependiente)
        //{
        //    ML.Result result = new ML.Result();
        //    try
        //    {
        //        using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
        //        {
        //            var query = context.Database.ExecuteSqlRaw($"DependienteAdd '{dependiente.Nombre}', '{dependiente.ApeliidoPaterno}','{dependiente.AppellidoMaterno}','{dependiente.FechaNacimiento}'," +
        //                $"'{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.Rfc}'," +
        //                $"{dependiente.empleado.IdEmpleado},{dependiente.dependienteTipo.IdDependienteTipo},");

        //            if (query >= 1)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "no se pudo agregar el dependiente";
        //            }

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;
        //}
        //public static ML.Result UpdateEF(ML.Dependiente dependiente)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
        //        {
        //            var updateResult = context.Database.ExecuteSqlRaw($"DependienteUpdate '{dependiente.Nombre}', '{dependiente.ApeliidoPaterno}','{dependiente.AppellidoMaterno}','{dependiente.FechaNacimiento}'," +
        //                $"'{dependiente.EstadoCivil}','{dependiente.Genero}','{dependiente.Telefono}','{dependiente.Rfc}'," +
        //                $"{dependiente.empleado.IdEmpleado},{dependiente.dependienteTipo.IdDependienteTipo},");

        //            if (updateResult >= 1)
        //            {
        //                result.Correct = true;
        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "no se pudo modificar el dependiente";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Correct = false;
        //        result.ErrorMessage = ex.Message;
        //        result.Ex = ex;

        //    }
        //    return result;

        //}
        public static ML.Result GetByIdEFDependiente(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var dependientes = context.Dependientes.FromSqlRaw($"DependienteGetByIdEmpleado {IdEmpleado}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (dependientes != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();

                        dependiente.IdDependiente = dependientes.IdDependiente;
                        dependiente.Nombre = dependientes.Nombre;
                        dependiente.ApeliidoPaterno = dependientes.ApellidoPaterno;
                        dependiente.AppellidoMaterno = dependientes.ApellidoMaterno;
                        dependiente.FechaNacimiento = dependientes.FechaNacimiento.ToString();
                        dependiente.EstadoCivil = dependientes.EstadoCivil;
                        dependiente.Genero = dependientes.Genero;
                        dependiente.Telefono = dependientes.Telefono;
                        dependiente.Rfc = dependientes.Rfc;

                        dependiente.empleado = new ML.Empleado();
                        dependiente.empleado.IdEmpleado = dependientes.IdEmpleado.Value;
                        //dependiente.empleado.Nombre = dependientes.EmpleadoNombre;
                       

                        dependiente.dependienteTipo = new ML.DependienteTipo();
                        dependiente.dependienteTipo.IdDependienteTipo = dependientes.IdDependienteTipo.Value;
                        dependiente.dependienteTipo.Nombre = dependientes.TipoDependiente;
                        result.Objects.Add(dependiente);
                        //result.Object = dependiente;
                        result.Correct = true;
                    }
                    else

                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de mostrar el dependiente";
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
