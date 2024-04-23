using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class PacienteConfiguracion : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder){
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Apellidos).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Nombres).IsRequired().HasMaxLength(60);
            builder.Property(x => x.Direccion).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Telefono).IsRequired(false).HasMaxLength(40);
            builder.Property(x => x.Genero).IsRequired().HasColumnType("char").HasMaxLength(1);
            builder.Property(x => x.Estado).IsRequired();
        }
    }
}