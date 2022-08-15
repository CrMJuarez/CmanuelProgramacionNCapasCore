using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL_WEBAPI.Controllers
{
    
    public class UsuarioController : ControllerBase
    {
        [HttpGet]
        [Route("api/usuario/GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Nombre = (usuario.Nombre == null) ? "" : usuario.Nombre;
            usuario.ApellidoPaterno = (usuario.ApellidoPaterno == null) ? "" : usuario.ApellidoPaterno;
            usuario.ApellidoMaterno = (usuario.ApellidoMaterno == null) ? "" : usuario.ApellidoMaterno;

            ML.Result result = BL.Usuario.GetAllEF(usuario);


            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("api/usuario/GetbyId/{IdUsuario}")]
        
        public IActionResult GetById(int IdUsuario)
        {
            var result = BL.Usuario.GetByIdEF(IdUsuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("api/usuario/Add")]

        
        public IActionResult Post([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.AddEF(usuario);

            if (result.Correct)
            {  
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpPut]
        [Route("api/usuario/Update")]

        public IActionResult Put([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.UpdateEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("api/usuario/Delete/{IdUsuario}")]

     
        public IActionResult Delete(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.IdUsuario = IdUsuario;
            var result = BL.Usuario.DeleteEF(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }


}

