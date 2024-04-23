using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class EspecialidadDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El Nombre es Requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "El Nombre debe ser minimo 1, maximo 60 caracteres")]
        public string NombreEspecialidad { get; set; }

        [Required(ErrorMessage = "La Descripcion es Requerida")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "La Descripcion debe ser minimo 1, maximo 100 caracteres")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El Estado es Requerido")]
        public int Estado { get; set; } // 1 - 0
    }
}