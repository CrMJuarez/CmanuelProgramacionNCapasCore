using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAllEF();


            if (result.Correct)
            {
                empleado.Empleados = result.Objects.ToList();

                return View(empleado);
            }
            else
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result resultPoliza = BL.Poliza.GetAll();
            empleado.Poliza = new ML.Poliza();
            ML.Result resultEmpresa = BL.Empresa.GetAll();
            empleado.Empresa = new ML.Empresa();
            if (IdEmpleado == null)
            {
                empleado.Poliza = new ML.Poliza();
                empleado.Poliza.Polizas = resultPoliza.Objects.ToList();
                empleado.Empresa = new ML.Empresa();
                empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
                return View(empleado);
            }
            else
            {
                ML.Result result = BL.Empleado.GetByIdEF(IdEmpleado.Value);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    empleado.Poliza.Polizas = resultPoliza.Objects.ToList();
                    empleado = (ML.Empleado)result.Object;
                    empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
                    return View(empleado);
                }
                else
                {
                    ViewBag.Message = result.ErrorMessage;
                    return View();
                }
            }
        }


        [HttpPost]

        public ActionResult Form(ML.Empleado empleado)
        {
            IFormFile file = Request.Form.Files["IFImage"];

            if (file != null)
            {

                byte[] ImagenBytes = ConvertToBytes(file);

                empleado.Foto = Convert.ToBase64String(ImagenBytes);
            }
            if (ModelState.IsValid)
            {
                if (empleado.IdEmpleado == 0)
                {

                    ML.Result result = BL.Empleado.AddEF(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se agrego correctamente el empleado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
                        return PartialView("Modal");
                    }
                }
                else
                {
                    ML.Result result = BL.Empleado.UpdateEF(empleado);
                    if (result.Correct)
                    {
                        ViewBag.Message = "Se actualizo correctamente el empleado";
                        return PartialView("Modal");
                    }
                    else
                    {
                        ViewBag.Message = "No se pudo actualizar el empleado";
                        return PartialView("Modal");

                    }
                }        
            }
            else
            {
                ML.Result resultEmpresa = BL.Empresa.GetAll();
                ML.Result resultPoliza = BL.Poliza.GetAll();
                empleado.Empresa.Empresas = resultEmpresa.Objects.ToList();
                empleado.Poliza.Polizas = resultPoliza.Objects.ToList();

                return View(empleado);

            }
        }

            [HttpPost]
        public ActionResult CargaMasiva()
        {
            
            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            try
            {

                IFormFile archivo = Request.Form.Files["Archivo"];
                using (StreamReader sr = new StreamReader(archivo.OpenReadStream()))
                {
                    string line;
                    line = sr.ReadLine();
                    while ((line = sr.ReadLine()) != null)
                    {
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

                        ML.Result result = BL.Empleado.AddEF(empleado);

                        if (!result.Correct) //si el resultado es diferente a correcto
                        {
                            resultErrores.Objects.Add(
                                "No se inserto el NumeroEmpleado " + empleado.NumeroEmpleado +
                                "No se inserto el Rfc " + empleado.Rfc +
                                "No se inserto el Nombre" + empleado.Nombre +
                                "No se inserto el ApellidoPaterno" + empleado.ApellidoPaterno +
                                "No se inserto el ApellidoMaterno " + empleado.ApellidoMaterno +
                                "No se inserto el Email" + empleado.Email +
                                "No se inserto el Telefono" + empleado.Telefono +
                                "No se inserto el FechaNacimiento " + empleado.FechaNacimiento +
                                "No se inserto el Nss" + empleado.Nss +
                                "No se inserto el FechaIngreso" + empleado.FechaIngreso +
                                "No se inserto el Foto " + empleado.Foto +
                                "No se inserto el Empresa" + empleado.Empresa.IdEmpresa +
                                "No se inserto el Poliza" + empleado.Poliza.IdPoliza);
                        } //Se le asigna agrega la lista de errores



                    }
                    if (resultErrores.Objects != null)
                    {

                    }

                    TextWriter tw = new StreamWriter(@"C:\Users\ALIEN09\Documents\Cmanuel\Errores.txt");
                    foreach (string error in resultErrores.Objects)
                    {
                        tw.WriteLine(error); //Se le concatenan todos los errores
                    }
                    tw.Close();

                    return PartialView("Modal");

                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                //Console.WriteLine("The file could not be read:");
                //Console.WriteLine(e.Message);
            }

            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.IdEmpleado = IdEmpleado;
            var result = BL.Empleado.DeleteEF(empleado);

            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente el usuario";

            }
            else
            {
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;

            }

            return PartialView("Modal");


        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }
}

