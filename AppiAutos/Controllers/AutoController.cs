using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using AppiAutos.Data;
using AppiAutos.Models;

namespace AppiAutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoController : ControllerBase
    {
        private readonly AutoData _autoData;
        public AutoController(AutoData autoData)
        {
            _autoData = autoData;
        }
        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            List<Auto> Lista = await _autoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Obtener(int id)
        {
            Auto objeto = await  _autoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);

        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Auto Objeto)
        {
            bool respuesta = await _autoData.Crear(Objeto);
            return StatusCode(StatusCodes.Status200OK, new {isSuccess = respuesta  });

        }
        [HttpPut]
        public async Task<IActionResult> Editar([FromBody] Auto Objeto)
        {
            bool respuesta = await _autoData.Editar(Objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _autoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });

        }
    }
}
