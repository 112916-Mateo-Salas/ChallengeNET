using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UserChallenge.Application.DTOs.Usuarios;
using UserChallenge.Application.Services;
using UserChallenge.Domain.Model;

namespace UserChallenge.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _service = usuarioService;
        }

        //Busqueda de Usuario por su ID
        /// <summary>
        /// Obtiene un usuario por su identificador.
        /// </summary>
        /// /// <param name="id">Identificador de usuario.</param>
        /// <returns>UsuarioDto con los datos del usuario.</returns>
        /// <response code="200">Devuelve el usuario.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UsuarioDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UsuarioDto>> GetUsuarioById(int id)
        {
            var usuario = await _service.GetUsuarioById(id);
            return Ok(usuario);
        }


        //Busqueda de Lista de Usuarios por su Nombre, Provincia o Ciudad
        /// <summary>
        /// Obtiene una lista de usuarios filtrada por nombre, provincia o ciudad.
        /// </summary>
        /// <param name="filtros">Filtros de búsqueda (nombre/provincia/ciudad).</param>
        /// <returns>Lista de usuarios que cumplen los filtros.</returns>
        /// <response code="200">Devuelve la lista (puede estar vacía).</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<UsuarioDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<UsuarioDto>>> GetUsuariosByFilters([FromQuery]FiltersUsuario filtros) { 
            var usuarios = await _service.GetAllUsuariosByFilters(filtros);
            return Ok(usuarios);
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="usuario">Datos del usuario a crear.</param>
        /// <returns>Objeto con la referencia al recurso creado.</returns>
        /// <response code="201">Recurso creado correctamente.</response>
        /// <response code="400">Solicitud inválida.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPost]
        [ProducesResponseType(typeof(CreatedUsuarioDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUsuario (RegisterUsuario usuario)
        {
            var usuarioNuevo =await _service.CreateUsuario(usuario);
            return CreatedAtAction(
                nameof(GetUsuarioById),
                new { id = usuarioNuevo.Id },
                usuarioNuevo// El Objeto creado recientemente
                );
        }


        /// <summary>
        /// Actualiza un usuario existente.
        /// </summary>
        /// <param name="id">Id del usuario a actualizar.</param>
        /// <param name="usuario">Datos a actualizar.</param>
        /// <response code="204">Actualización correcta, sin contenido de retorno.</response>
        /// <response code="400">Solicitud inválida.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateUsuario (int id, [FromBody] UpdateUsuario usuario)
        {
            await _service.UpdateUsuario(id, usuario);

            return NoContent();
        }


        /// <summary>
        /// Elimina un usuario por su id.
        /// </summary>
        /// <param name="id">Id del usuario a eliminar.</param>
        /// <response code="204">Eliminación correcta.</response>
        /// <response code="404">Usuario no encontrado.</response>
        /// <response code="500">Error interno del servidor.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUsuario (int id)
        {
            await _service.DeleteUsuario(id);
            return NoContent();
        }
    }
}
