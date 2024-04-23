using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Entidades
{
    public class HistoriaClinica
    {
        [Key]
        public Guid Id { get; set; } // Crea como una cadena de caracteres unica

        [Required(ErrorMessage = "Paciente es requerido")]
        public int PacienteId { get; set; }

        [ForeignKey("PacienteId")]
        public Paciente Paciente { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }
    }
}