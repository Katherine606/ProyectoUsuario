using System.ComponentModel.DataAnnotations;

namespace UsuarioAPI.Models.Dtos
{
    public class UsuarioActualizarDto
    {

        public int id { get; set; }

        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
        public string? nombres { get; set; } = string.Empty;
     
        [StringLength(50, ErrorMessage = "El apellido no puede superar los 50 caracteres.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
        public string? apellidos { get; set; } = string.Empty;
     
        [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
        [StringLength(100, ErrorMessage = "El correo es demasiado largo.")]
        public string? correo { get; set; } = string.Empty;
  
        [StringLength(30, MinimumLength = 3, ErrorMessage = "El username debe tener entre 3 y 30 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$",
        ErrorMessage = "El username solo puede contener letras, números y guion bajo.")]
        public string? username { get; set; } = string.Empty;
  
        [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos una letra y un número.")]
        public string? password_hash { get; set; } = string.Empty;
    }
}
