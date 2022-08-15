using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        public ActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAllEF();
            if (result.Correct)
            {
                empresa.Empresas = result.Objects.ToList();
                return View(empresa);
            }
            else
                ViewBag.Message = "ocurrio un problema" + result.ErrorMessage;
            return PartialView("Modal");

        }
        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == null)
            {
                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.GetByIdEF(IdEmpresa.Value);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    return View(empresa);
                }
                ViewBag.Message = result.ErrorMessage;
                return View();
            }
        }
        [HttpPost]

        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile file = Request.Form.Files["IFImage"];

            if (file != null)
            {

                byte[] ImagenBytes = ConvertToBytes(file);

                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }
            if (empresa.IdEmpresa == null)
            {
                ML.Result result = BL.Empresa.AddEF(empresa);
                if (result.Correct)
                {
                    ViewBag.Message = "Se agrego correctamente la aseguradora";
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
                ML.Result result = BL.Empresa.UpdateEF(empresa);
                if (result.Correct)
                {
                    ViewBag.Message = "Se actualizo correctamente la aseguradora";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "No se pudo actualizar la aseguradora";
                    return PartialView("Modal");

                }
            }

        }

        [HttpGet]
        public ActionResult Delete(int IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            empresa.IdEmpresa = IdEmpresa;
            var result = BL.Empresa.DeleteEF(empresa);

            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente la aseguradora";

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
