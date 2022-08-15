using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace BL
{
    public class Empleado
    {
        public static ML.Result AddEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.NumeroEmpleado}', '{empleado.Rfc}', '{empleado.Nombre}'," +
                        $"'{ empleado.ApellidoPaterno}', '{ empleado.ApellidoMaterno}', '{ empleado.Email}','{ empleado.Telefono}','{ empleado.FechaNacimiento}','{ empleado.Nss}'," +
                        $"'{ empleado.FechaIngreso}','{empleado.Foto}',{ empleado.Empresa.IdEmpresa},{ empleado.Poliza.IdPoliza}");

                    if (query >= 1)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se inserto el empleado";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;
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
                    var empleados = context.Empleados.FromSqlRaw($"EmpleadoGetAll").ToList();
                    result.Objects = new List<object>();
                    if (empleados != null)
                    {
                        foreach (var obj in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NumeroEmpleado = obj.NumeroEmpleado;
                            empleado.Rfc = obj.Rfc;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;
                            empleado.Email = obj.Email;
                            empleado.Telefono = obj.Telefono;
                            empleado.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            empleado.Nss = obj.Nss;
                            empleado.FechaIngreso = obj.FechaIngreso.Value.ToString("dd-MM-yyyy");
                            empleado.Foto = obj.Foto;

                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = obj.IdEmpresa.Value;
                            empleado.Empresa.Nombre = obj.EmpresaNombre;

                            empleado.Poliza = new ML.Poliza();
                            empleado.Poliza.IdPoliza = obj.IdPoliza.Value;
                            empleado.Poliza.Nombre = obj.PolizaNombre;

                            result.Objects.Add(empleado);

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
        public static ML.Result GetByIdEF(int IdEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var objEmpleado = context.Empleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (objEmpleado != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();
                        empleado.IdEmpleado = objEmpleado.IdEmpleado;
                        empleado.NumeroEmpleado = objEmpleado.NumeroEmpleado;
                        empleado.Rfc = objEmpleado.Rfc;
                        empleado.Nombre = objEmpleado.Nombre;
                        empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                        empleado.ApellidoMaterno = objEmpleado.ApellidoMaterno;
                        empleado.Email = objEmpleado.Email;
                        empleado.Telefono = objEmpleado.Telefono;
                        empleado.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        empleado.Nss = objEmpleado.Nss;
                        empleado.FechaIngreso = objEmpleado.FechaIngreso.Value.ToString("dd-MM-yyyy");
                        empleado.Foto = objEmpleado.Foto;

                        empleado.Empresa = new ML.Empresa();
                        empleado.Empresa.IdEmpresa = objEmpleado.IdEmpresa.Value;
                        empleado.Empresa.Nombre = objEmpleado.EmpresaNombre;

                        empleado.Poliza = new ML.Poliza();
                        empleado.Poliza.IdPoliza = objEmpleado.IdPoliza.Value;
                        empleado.Poliza.Nombre = objEmpleado.PolizaNombre;

                        result.Object = empleado;
                        result.Correct = true;


                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Empleado";
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

        public static ML.Result UpdateEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var updateResult = context.Database.ExecuteSqlRaw($"EmpleadoUpdate '{empleado.NumeroEmpleado}', '{empleado.Rfc}', '{empleado.Nombre}'," +
                        $"'{ empleado.ApellidoPaterno}', '{ empleado.ApellidoMaterno}', '{ empleado.Email}','{ empleado.Telefono}','{ empleado.FechaNacimiento}','{ empleado.Nss}'," +
                        $"'{ empleado.FechaIngreso}','{empleado.Foto}',{ empleado.Empresa.IdEmpresa},{ empleado.Poliza.IdPoliza},{empleado.IdEmpleado}");


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

        public static ML.Result DeleteEF(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {empleado.IdEmpleado}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo eliminar el empleado";
                    }
                    result.Correct = true;
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

        public static ML.Result ConvertirExceltoDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Hoja 1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;


                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableEmpleado = new DataTable();

                        da.Fill(tableEmpleado);

                        if (tableEmpleado.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableEmpleado.Rows)
                            {
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.NumeroEmpleado = row[0].ToString();
                                empleado.Rfc = row[1].ToString();
                                empleado.Nombre = row[2].ToString();
                                empleado.ApellidoPaterno = row[3].ToString();
                                empleado.ApellidoMaterno = row[4].ToString();
                                empleado.Email = row[5].ToString();
                                empleado.Telefono = row[6].ToString();
                                //convertir la fecha
                                //usuarioDL.FechaNacimiento = DateTime.ParseExact(usuario.FechaNacimiento, "dd/mm/yyyy", CultureInfo.InvariantCulture);
                                //usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                                //DateTime.ParseExact(empleado.FechaNacimiento, "dd/mm/yyyy", CultureInfo.InvariantCulture).ToString();
                                empleado.FechaNacimiento = row[7].ToString();
                                empleado.Nss = row[8].ToString();
                                empleado.FechaIngreso = row[9].ToString();
                                empleado.Foto = null;
                                empleado.Empresa = new ML.Empresa();
                                empleado.Empresa.IdEmpresa = int.Parse(row[11].ToString());
                                empleado.Poliza = new ML.Poliza();
                                empleado.Poliza.IdPoliza = int.Parse(row[12].ToString());

                                result.Objects.Add(empleado);
                            }

                            result.Correct = true;

                        }

                        result.Object = tableEmpleado;

                        if (tableEmpleado.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Empleado empleado in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (empleado.NumeroEmpleado == "")
                    {
                        error.Mensaje += "Ingresar el nombre  ";
                    }
                    if (empleado.Rfc == "")
                    {
                        error.Mensaje += "Ingresar el rfc ";
                    }
                    if (empleado.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el Nombre ";
                    }
                    if (empleado.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno ";
                    }
                    if (empleado.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno ";
                    }
                    if (empleado.Email == "")
                    {
                        error.Mensaje += "Ingresar el Email ";
                    }
                    if (empleado.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el Telefono ";
                    }
                    if (empleado.FechaNacimiento.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Fecha Nacimiento ";
                    }
                    if (empleado.Nss == "")
                    {
                        error.Mensaje += "Ingresar el Nss ";
                    }
                    if (empleado.FechaIngreso.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el Fecha Ingreso ";
                    }
                    if (empleado.Foto =="")
                    //agregar to string y probar en caso de error ToString()
                    {
                        error.Mensaje += "Ingresar la Foto ";
                    }
                    if (empleado.Empresa.IdEmpresa.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdEmpresa ";
                    }
                    if (empleado.Poliza.IdPoliza.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el IdPoliza ";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        //public static ML.Result GetByIdEFDependiente(int IdEmpleado)
        //{
        //    ML.Result result = new ML.Result();

        //    try
        //    {
        //        using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
        //        {
        //            var objEmpleado = context.Empleados.FromSqlRaw($"DependienteGetByIdEmpleado {IdEmpleado}").AsEnumerable().FirstOrDefault();
        //            result.Objects = new List<object>();
        //            if (objEmpleado != null)
        //            {
        //                ML.Empleado empleado = new ML.Empleado();
        //                //ML.Dependiente dependiente = new ML.Dependiente();
        //                empleado.IdEmpleado = objEmpleado.IdEmpleado;
        //                empleado.NumeroEmpleado = objEmpleado.NumeroEmpleado;
        //                empleado.Rfc = objEmpleado.Rfc;
        //                empleado.Nombre = objEmpleado.Nombre;
        //                empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
        //                empleado.ApellidoMaterno = objEmpleado.ApellidoMaterno;
        //                empleado.Email = objEmpleado.Email;
        //                empleado.Telefono = objEmpleado.Telefono;
        //                empleado.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
        //                empleado.Nss = objEmpleado.Nss;
        //                empleado.FechaIngreso = objEmpleado.FechaIngreso.Value.ToString("dd-MM-yyyy");
        //                empleado.Foto = objEmpleado.Foto;

        //                empleado.Empresa = new ML.Empresa();
        //                empleado.Empresa.IdEmpresa = objEmpleado.IdEmpresa.Value;
        //                empleado.Empresa.Nombre = objEmpleado.EmpresaNombre;

        //                empleado.Poliza = new ML.Poliza();
        //                empleado.Poliza.IdPoliza = objEmpleado.IdPoliza.Value;
        //                empleado.Poliza.Nombre = objEmpleado.PolizaNombre;

        //                empleado.Dependiente= new ML.Dependiente();
        //                empleado.Dependiente.IdDependiente = objEmpleado.IdDependiente.Value;
        //                empleado.Dependiente.Nombre = objEmpleado.DependienteNombre;
        //                empleado.Dependiente.ApeliidoPaterno = objEmpleado.ApellidoPaterno;
        //                empleado.Dependiente.AppellidoMaterno = objEmpleado.ApellidoMaterno;
        //                empleado.Dependiente.FechaNacimiento = objEmpleado.FechaNacimiento.Value.ToString("dd-MM-yyyy");
        //                empleado.Dependiente.EstadoCivil = objEmpleado.EstadoCivil;
        //                empleado.Dependiente.Genero = objEmpleado.Genero;
        //                empleado.Dependiente.Telefono = objEmpleado.Telefono;
        //                empleado.Dependiente.Rfc = objEmpleado.Rfc;

        //                empleado.DependienteTipo= new ML.DependienteTipo();
        //                empleado.DependienteTipo.IdDependienteTipo = objEmpleado.IdDependienteTipo.Value;
        //                empleado.DependienteTipo.Nombre = objEmpleado.Nombre;
                        
                        
        //                result.Object = empleado;
        //                result.Correct = true;


        //            }
        //            else
        //            {
        //                result.Correct = false;
        //                result.ErrorMessage = "No se encontraron dependientes en la tabla Empleado";
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

    }
}