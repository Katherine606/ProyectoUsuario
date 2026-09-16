using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using UsuarioAPI.Exceptions;
using UsuarioAPI.Models.Dtos;
using UsuarioAPI.Models.Entities;
using UsuarioAPI.Repositories;

namespace UsuarioAPI.Services
{
    public class UsuarioServ
    {

        private readonly UsuarioRepo _usuarioRepo;
        private readonly ValidacionesServ _validacionesServ;

        public UsuarioServ (UsuarioRepo usuarioRepo, ValidacionesServ validacionesServ) 
        {
            _usuarioRepo = usuarioRepo;
            _validacionesServ = validacionesServ;
        }

        public async Task<IEnumerable<UsuarioListaDto>> ObtenerUsuarios()
        {
            var usuarios = await _usuarioRepo.ListarUsuariosAsync();

            return usuarios.Select(u => new UsuarioListaDto
            {
                id = u.id,
                nombres = u.nombres,
                apellidos = u.apellidos,
                username = u.username,
                correo = u.correo,
                fecha_creacion = u.fecha_creacion.ToString("dd/MM/yyyy"),
                estado = u.estado
            });
        }

        public async Task<int> CrearUsuario(UsuarioCrearDto dto)
        {
            await _validacionesServ.ValidarCorreoAsync(dto.correo);
            await _validacionesServ.ValidarUsernameAsync(dto.username);

            string passwordHashSeguro = BCrypt.Net.BCrypt.HashPassword(dto.password_hash);

            var usuario = new Usuario
            {
                nombres = dto.nombres,
                apellidos = dto.apellidos,
                correo = dto.correo,
                username = dto.username,
                password_hash = passwordHashSeguro,
                estado = "A" 
            };

            int nuevoUsuarioId = await _usuarioRepo.CrearUsuarioAsync(usuario);

            return nuevoUsuarioId;
        }

        public async Task ActualizarUsuario(UsuarioActualizarDto dto)
        {
            bool existe = await _usuarioRepo.ExisteUsuarioAsync(dto.id);
            if (!existe)
            {
                throw new ApiException($"El usuario con el ID {dto.id} no existe", 404);
            }

            await _validacionesServ.ValidarCorreoParaActualizarAsync(dto.id, dto.correo);
            await _validacionesServ.ValidarUsernameParaActualizarAsync(dto.id, dto.username); 

            string? nombresNuevo = string.IsNullOrWhiteSpace(dto.nombres) ? null : dto.nombres;
            string? apellidosNuevo = string.IsNullOrWhiteSpace(dto.apellidos) ? null : dto.apellidos;
            string? correoNuevo = string.IsNullOrWhiteSpace(dto.correo) ? null : dto.correo;
            string? usernameNuevo = string.IsNullOrWhiteSpace(dto.username) ? null : dto.username;
            string? passwordHashNueva = null;

            if (!string.IsNullOrEmpty(dto.password_hash))
            {
                passwordHashNueva = BCrypt.Net.BCrypt.HashPassword(dto.password_hash);
            }

            var usuarioActualizado = new UsuarioActualizarDto
            {
                id = dto.id,
                nombres = nombresNuevo,
                apellidos = apellidosNuevo,
                correo = correoNuevo,
                username = usernameNuevo,
                password_hash = passwordHashNueva
            };

            await _usuarioRepo.ActualizarUsuario(usuarioActualizado);
        }

        public async Task<int> EliminarUsuario(int id)
        {

            bool existe = await _usuarioRepo.ExisteUsuarioAsync(id);
            if (!existe)
            {
                throw new ApiException($"El usuario con el ID {id} no existe", 404);
            }

            await _validacionesServ.ValidarUsuarioEliminado(id);

            return await _usuarioRepo.EliminarUsuario(id);
        }

    }
}
