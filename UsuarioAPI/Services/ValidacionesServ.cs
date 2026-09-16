using UsuarioAPI.Exceptions;
using UsuarioAPI.Models.Entities;
using UsuarioAPI.Repositories;

namespace UsuarioAPI.Services
{
    public class ValidacionesServ
    {

        private readonly UsuarioRepo _usuarioRepo;

        public ValidacionesServ (UsuarioRepo usuarioRepo)
        {
            _usuarioRepo = usuarioRepo;
        }


        public async Task ValidarCorreoAsync(string correo)
        {
            var usuarios = await _usuarioRepo.ObtenerUsuariosAsync();
            bool correoExiste = usuarios.Any(u => u.correo.Equals(correo, StringComparison.OrdinalIgnoreCase));

            if (correoExiste)
            {
                throw new ApiException("Este correo ya se encuentra registrado", 400);
            }
        }

   
        public async Task ValidarUsernameAsync(string username)
        {
            var usuarios = await _usuarioRepo.ObtenerUsuariosAsync();
            bool usernameExiste = usuarios.Any(u => u.username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (usernameExiste)
            {
                throw new ApiException("Este username ya se encuentra registrado", 400);
            }
        }


        public async Task ValidarCorreoParaActualizarAsync(int idUsuario, string? correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return;

            var usuarios = await _usuarioRepo.ObtenerUsuariosAsync();
            
            bool correoDuplicado = usuarios.Any(u => u.id != idUsuario && u.correo.Equals(correo, StringComparison.OrdinalIgnoreCase));

            if (correoDuplicado)
            {
                throw new ApiException("Este correo ya se encuentra registrado por otro usuario", 400);
            }
        }

        public async Task ValidarUsernameParaActualizarAsync(int idUsuario, string? username)
        {
            if (string.IsNullOrWhiteSpace(username)) return;

            var usuarios = await _usuarioRepo.ObtenerUsuariosAsync();

            bool usernameDuplicado = usuarios.Any(u => u.id != idUsuario && u.username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (usernameDuplicado)
            {
                throw new ApiException("Este username ya se encuentra registrado por otro usuario", 400);
            }
        }

        public async Task ValidarUsuarioEliminado(int idUsuario)
        {

            var usuarios = await _usuarioRepo.ObtenerUsuariosAsync();

            bool usuarioEliminado = usuarios.Any(u => u.id == idUsuario && u.estado == "N");

            if (usuarioEliminado)
            {
                throw new ApiException("Este usuario ya se encuentra eliminado", 404);
            }
        }

    }
}
