using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entidades;

namespace Data.Configuraciones
{
    public class AntedecenteConfiguracion : IEntityTypeConfiguration<Antecedente>
    {
        public void Configure(EntityTypeBuilder<Antecedente> builder){
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.HistoriaClinicaId).IsRequired();

            // Relaciones
            builder.HasOne(x => x.HistoriaClinica).WithMany()
                    .HasForeignKey(x => x.HistoriaClinicaId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}