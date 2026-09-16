using Dapper;
using System.Data;
using UsuarioAPI.Models.Dtos;
using UsuarioAPI.Models.Entities;

namespace UsuarioAPI.Repositories
{
    public class UsuarioRepo
    {
        private readonly IDbConnection _context;

        public UsuarioRepo(IDbConnection context)
        {
            _context = context;
        }
        public async Task<bool> ExisteUsuarioAsync(int id)
        {
            var query = "SELECT COUNT(1) FROM Usuario WHERE id = @id";
            int count = await _context.ExecuteScalarAsync<int>(query, new { id });
            return count > 0;
        }

        //listar usuarios
        public async Task<IEnumerable<Usuario>> ObtenerUsuariosAsync()
        {
            var query = "SELECT id, nombres, apellidos, correo, username, password_hash, rol, fecha_creacion, estado FROM Usuario";
            return await _context.QueryAsync<Usuario>(query);
        }

        public async Task<IEnumerable<Usuario>> ListarUsuariosAsync()
        {
            var query = "SELECT id, nombres, apellidos, correo, username, password_hash, rol, fecha_creacion, estado FROM Usuario WHERE Estado != 'N'";
            return await _context.QueryAsync<Usuario>(query);
        }

        //crear usuario
        public async Task<int> CrearUsuarioAsync(Usuario usuario)
        {
            var query = "INSERT INTO Usuario (nombres, apellidos, correo, username, password_hash) VALUES (@nombres, @apellidos, @correo, @username, @password_hash); SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _context.ExecuteScalarAsync<int>(query, usuario);
        }

        public async Task ActualizarUsuario (UsuarioActualizarDto dto)
        {
            var query = @"UPDATE Usuario SET nombres = COALESCE(@nombres, nombres),
                      apellidos = COALESCE(@apellidos, apellidos),
                      correo = COALESCE(@correo, correo),
                      username = COALESCE(@username, username),
                      password_hash = COALESCE(@password_hash, password_hash) 
                  WHERE id = @id";

            await _context.ExecuteAsync(query, new
            {
                id= dto.id,
                nombres = dto.nombres,
                apellidos = dto.apellidos,
                correo = dto.correo,
                username = dto.username,
                password_hash = dto.password_hash
            });
        }

        public async Task<int> EliminarUsuario (int id)
        {
            var query = "UPDATE Usuario SET Estado = 'N' Where id = @id";
            return await _context.ExecuteAsync(query, new { id });
        }
    }
}
