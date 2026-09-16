namespace UsuarioAPI.Models.Entities
{
    public class Usuario
    {

        public int id { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password_hash { get; set; } = string.Empty;
        public string rol { get; set; } = string.Empty;
        public DateTime fecha_creacion { get; set; }
        public string estado { get; set; } = "A";

    }
}
