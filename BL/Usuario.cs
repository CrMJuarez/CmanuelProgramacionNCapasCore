using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result AddEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'," +
                        $"'{ usuario.Sexo}', '{ usuario.CURP}', '{ usuario.UserName}','{ usuario.Email}','{ usuario.Telefono}','{ usuario.Celular}'," +
                        $"'{ usuario.FechaNacimiento}','{usuario.Imagen}',{ usuario.Rol.IdRol},{ usuario.Estatus = true},'{usuario.Password}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}'" +
                        $",{usuario.Direccion.Colonia.IdColonia}");

                    //var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'," +
                    //                                           $"'{ usuario.Sexo}', '{ usuario.CURP}', '{ usuario.UserName}','{ usuario.Email}','{ usuario.Telefono}','{ usuario.Celular}'," +
                    //                                           $"'{ usuario.FechaNacimiento}','{usuario.Imagen}',{ usuario.Rol.IdRol},{ usuario.Estatus = true},'{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}'" +
                    //                                           $",{usuario.Direccion.Colonia.IdColonia}");

                    if (query >= 1)
                    {
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se inserto el usuario";
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
        public static ML.Result GetAllEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            usuario = new ML.Usuario();
                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Sexo = obj.Sexo;
                            usuario.CURP = obj.Curp;
                            usuario.UserName = obj.UserName;
                            usuario.Email = obj.Email;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.FechaNacimiento = obj.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                            usuario.Password = obj.Password;
                            //se debe inicializar el modelo rol para que tome el valor: usuario.Rol = new ML.Rol();
                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.RolNombre;
                            usuario.Imagen = obj.Imagen;
                            usuario.Estatus = obj.Estatus.Value;
                            //usuario.Imagen = Convert.FromBase64String(obj.Imagen.ToString());
                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.CalleNombre;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;
                            usuario.Direccion.Colonia.Nombre = obj.ColoniaNombre;
                            usuario.Direccion.Colonia.CodigoPostal = obj.CodigoPostal;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdMunicipio;
                            usuario.Direccion.Colonia.Municipio.Nombre = obj.MunicipioNombre;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.EstadoNombre;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.PaisNombre;

                            result.Objects.Add(usuario);


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
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.CURP = objUsuario.Curp;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Email = objUsuario.Email;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.FechaNacimiento = objUsuario.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;
                        usuario.Rol.Nombre = objUsuario.RolNombre;
                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Estatus = objUsuario.Estatus.Value;
                        usuario.Password = objUsuario.Password;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion;
                        usuario.Direccion.Calle = objUsuario.CalleNombre;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia;
                        usuario.Direccion.Colonia.Nombre = objUsuario.ColoniaNombre;
                        usuario.Direccion.Colonia.CodigoPostal = objUsuario.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.MunicipioNombre;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.PaisNombre;
                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Materia";
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
        public static ML.Result DeleteEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se pudo eliminar el usuario";
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
        public static ML.Result UpdateEF(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var updateResult = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario},'{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'," +
                        $"'{ usuario.Sexo}', '{ usuario.CURP}','{ usuario.UserName}','{ usuario.Email}','{ usuario.Telefono}','{ usuario.Celular}'," +
                        $"'{ usuario.FechaNacimiento}','{usuario.Imagen}',{ usuario.Rol.IdRol},{ usuario.Estatus},'{usuario.Password}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}'" +
                        $",{usuario.Direccion.Colonia.IdColonia},{usuario.Direccion.IdDireccion}");

                    //var query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}'," +
                    //    $"'{ usuario.Sexo}', '{ usuario.CURP}', '{ usuario.UserName}','{ usuario.Email}','{ usuario.Telefono}','{ usuario.Celular}'," +
                    //    $"'{ usuario.FechaNacimiento}','{usuario.Imagen}',{ usuario.Rol.IdRol},{ usuario.Estatus = true},'{usuario.Password}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}'" +
                    //    $",{usuario.Direccion.Colonia.IdColonia}");


                    if (updateResult >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizo el usuario";
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
        public static ML.Result GetByEmail(string email)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.CManuelProgramacionNCapasContext context = new DL.CManuelProgramacionNCapasContext())
                {
                    var objUsuario = context.Usuarios.FromSqlRaw($"UsuarioGetByEmail '{email}'").AsEnumerable().FirstOrDefault();
                    if (objUsuario != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = objUsuario.IdUsuario;
                        usuario.Nombre = objUsuario.Nombre;
                        usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                        usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                        usuario.Sexo = objUsuario.Sexo;
                        usuario.CURP = objUsuario.Curp;
                        usuario.UserName = objUsuario.UserName;
                        usuario.Email = objUsuario.Email;
                        usuario.Telefono = objUsuario.Telefono;
                        usuario.Celular = objUsuario.Celular;
                        usuario.FechaNacimiento = objUsuario.FechaNacimiento.Value.ToString("dd-MM-yyyy");
                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = objUsuario.IdRol.Value;
                        usuario.Rol.Nombre = objUsuario.RolNombre;
                        usuario.Imagen = objUsuario.Imagen;
                        usuario.Estatus = objUsuario.Estatus.Value;
                        usuario.Password = objUsuario.Password;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = objUsuario.IdDireccion;
                        usuario.Direccion.Calle = objUsuario.CalleNombre;
                        usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                        usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = objUsuario.IdColonia;
                        usuario.Direccion.Colonia.Nombre = objUsuario.ColoniaNombre;
                        usuario.Direccion.Colonia.CodigoPostal = objUsuario.CodigoPostal;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = objUsuario.IdMunicipio;
                        usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.MunicipioNombre;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = objUsuario.IdEstado;
                        usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.EstadoNombre;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = objUsuario.IdPais;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.PaisNombre;
                        result.Object = usuario;
                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros en la tabla Usuario";
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
