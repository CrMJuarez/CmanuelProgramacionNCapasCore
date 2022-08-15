using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DependienteController : Controller
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

        public ActionResult GetAllDependiente(int? IdEmpleado)
        {
               
                ML.Result result = BL.Dependiente.GetByIdEFDependiente(IdEmpleado.Value);
                ML.Result resultempleado = BL.Empleado.GetByIdEF(IdEmpleado.Value);

                if (result.Correct)
                {
                ML.Dependiente dependiente = new ML.Dependiente();
                //dependiente = (ML.Dependiente)result.Object;
                dependiente.Dependientes= result.Objects;
                dependiente.empleado = new ML.Empleado();
                dependiente.empleado = ((ML.Empleado)resultempleado.Object);
                return View(dependiente);
                }
                else
                ViewBag.Message = result.ErrorMessage;
                return View();

            }
        }
    }

