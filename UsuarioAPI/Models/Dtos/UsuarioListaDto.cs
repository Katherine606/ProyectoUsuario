namespace UsuarioAPI.Models.Dtos
{
    public class UsuarioListaDto
    {
        public int id { get; set; }
        public string nombres { get; set; } = string.Empty;
        public string apellidos { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string fecha_creacion { get; set; } = string.Empty;
        public string estado { get; set; } = string.Empty;
    }
}
