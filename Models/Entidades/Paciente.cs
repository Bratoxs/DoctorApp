using System.ComponentModel.DataAnnotations;

namespace Models.Entidades
{
    public class Paciente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Apellidos es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Apellidos debe ser minimo 1 maximo 60 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombres es requerido")]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Nombres debe ser minimo 1 maximo 60 caracteres")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Direccion es requerido")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Direccion debe ser minimo 1 maximo 100 caracteres")]
        public string Direccion { get; set; }

        [StringLength(40, MinimumLength = 1, ErrorMessage = "telefono debe ser minimo 1 maximo 40 caracteres")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "Genero es requerido")]
        public char Genero { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public HistoriaClinica HistoriaClinica { get; set; }
    }
}