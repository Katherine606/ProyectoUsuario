using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UsuarioAPI.Models.Dtos;
using UsuarioAPI.Models.Entities;
using UsuarioAPI.Services;

namespace UsuarioAPI.Controllers
{
    public class UsuarioController : ControllerBase
    {

        private readonly UsuarioServ _usuarioServ;

        public UsuarioController(UsuarioServ usuarioServ)
        {
            _usuarioServ = usuarioServ;
        }

        [HttpGet("Listar")]
        public async Task<ActionResult> ObtenerUsuarios()
        {
            var lista = await _usuarioServ.ObtenerUsuarios();
           return Ok(lista);
        }

        [HttpPost("crear")]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioCrearDto dto)
        {
            var nuevoId = await _usuarioServ.CrearUsuario(dto);
            return Ok(new { mensaje = $"Usuario creado con éxito", id = nuevoId });
        }

        [HttpPatch("actualizar")]
        public async Task<ActionResult> ActualizarUsuario([FromBody] UsuarioActualizarDto dto)
        {
     
            await _usuarioServ.ActualizarUsuario(dto);

            return Ok(new { mensaje = $"Usuario con ID {dto.id} actualizado con éxito" });
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<ActionResult> EliminarUsuario(int id)
        {

            await _usuarioServ.EliminarUsuario(id);

            return Ok(new { mensaje = $"Usuario con ID {id} eliminado con éxito" });
        }
    }
}
