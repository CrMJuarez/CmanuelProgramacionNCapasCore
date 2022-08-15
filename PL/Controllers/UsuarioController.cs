using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        //se agregar para consumir servicios
        private readonly IConfiguration _configuration;

        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            //usuario.Nombre=(usuario.Nombre==null)?"":usuario.Nombre;
            //usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            //usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            //ML.Result resultUsuarios = BL.Usuario.GetAllEF(usuario);
            ML.Result resultUsuario = new ML.Result();
            resultUsuario.Objects = new List<Object>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                var responseTask = client.GetAsync("api/usuario/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        resultUsuario.Objects.Add(resultItemList);
                    }
                }
                usuario.Usuarios = resultUsuario.Objects;
                //descomentar cuando no se ocupen servicios
                //if (result.Correct)
                //{
                //    usuario.Usuarios = result.Objects.ToList();

                //    return View(usuario);
                //}
                //else
                //    ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
                //return PartialView("Modal");
            }
            return View(usuario);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAllEF(usuario);
            usuario.Usuarios = result.Objects;
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result resultRol = BL.Rol.GetAll();
            usuario.Rol = new ML.Rol();
            ML.Result resultPais = BL.Pais.GetAll();

            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

            if (IdUsuario == null)
            {
                ViewBag.Titulo = "Agregar un Usuario";
                ViewBag.Accion = "Agregar";
                usuario.Rol = new ML.Rol();
                usuario.Rol.Roles = resultRol.Objects.ToList();

                //usuario.Pais = new ML.Pais();
                //usuario.Pais.Paises = resultPais.Objects.ToList();
                return View(usuario);
            }
            else
            {

                ViewBag.Titulo = "Actualizar un Usuario";
                ViewBag.Accion = "Actualizar";

                //ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.GetAsync("api/usuario/GetById/" + IdUsuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {

                        var usuarioTask = result.Content.ReadAsAsync<ML.Result>();
                        usuarioTask.Wait();
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(usuarioTask.Result.Object.ToString());
                        resultItemList.Rol.Roles = resultRol.Objects.ToList();



                        //usuario = ((ML.Usuario)result.Object);
                        //usuario.Rol.Roles = resultRol.Objects.ToList();

                        ML.Result resultEstado = BL.Estado.GetByIdEstado(resultItemList.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                        ML.Result resultMunicipio = BL.Municipio.GetByIdMunicipio(resultItemList.Direccion.Colonia.Municipio.Estado.IdEstado);
                        ML.Result resultColonia = BL.Colonia.GetByIdColonia(resultItemList.Direccion.Colonia.Municipio.IdMunicipio);

                        resultItemList.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects.ToList();
                        resultItemList.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects.ToList();
                        resultItemList.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects.ToList();
                        resultItemList.Direccion.Colonia.Colonias = resultColonia.Objects.ToList();


                        return View(resultItemList);
                    }
                    else
                    {
                        ViewBag.Message = "error al traer al usuario";
                        return View();
                    }
                }
            }
        }

        [HttpPost]

        public ActionResult Form(ML.Usuario usuario)
        {

            IFormFile file = Request.Form.Files["IFImage"];

            if (file != null)
            {

                byte[] ImagenBytes = ConvertToBytes(file);

                usuario.Imagen = Convert.ToBase64String(ImagenBytes);
            }
            //si el id de usuario es NULL=NULO SE AGREGA EL USUARIO, SI EL ID DE USUARIO 
            //ES DIFERENTE DE NULL= NULO ES DECIR TRAE UN ID EXISTENTE SE ACTUALIZA EN EL ELSE
            if (usuario.IdUsuario == null)
            {
                // ML.Result result = BL.Usuario.AddEF(usuario);


                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Usuario>("api/usuario/Add", usuario);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se agrego correctamente el usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "ocurrio un problema"; //+ result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }

            }
            //quitar esto o comentar cuado s eimplemente el update

            //return View(usuario);
            //ACTUALIZA USUARIO
            else
            {
                //ML.Result result = BL.Usuario.UpdateEF(usuario);
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration["WebAPI"]);

                    //CONEXION CON MI SERVICIO
                    var postTask = client.PutAsJsonAsync<ML.Usuario>("api/usuario/Update/", usuario);
                    postTask.Wait();
                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Message = "Se actualizo el usuario";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se pudo actualizar el usuario";
                        return PartialView("Modal");

                    }
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            
            // var result = BL.Usuario.DeleteEF(usuario);
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_configuration["WebAPI"]);

                //CONEXION CON MI SERVICIO
                var postTask = client.DeleteAsync("api/usuario/Delete/" + IdUsuario);
                postTask.Wait();
                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Message = "Se elimino correctamente el usuario";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "ocurrio un problema";
                    return PartialView("Modal");
                }
            }

        }

        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdEstado(IdPais);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdMunicipio(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdColonia(IdMunicipio);

            return Json(result.Objects);
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

        public ActionResult UpdateEstatus(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if(result.Correct)
            {
                ML.Usuario usuario = new ML.Usuario();
                usuario=((ML.Usuario)result.Object);
                usuario.Estatus = usuario.Estatus ? false : true;
                ML.Result resultUpdate = BL.Usuario.UpdateEF(usuario);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo el Estatus";
                }
                else 
                {
                    ViewBag.Message = "Problema al actualizar";
                }

            }
            return PartialView("Modal");
        }

    }
}

