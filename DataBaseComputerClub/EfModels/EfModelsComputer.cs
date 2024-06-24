using Domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace СomputerСlub.Persistence.EfModels
{
    internal class EfModelsComputer : IEntityTypeConfiguration<Computer>
    {
        public void Configure(EntityTypeBuilder<Computer> builder)
        {
            builder.HasKey(p => p.IdPC);

            builder.Property(p => p.ComputerStatus)
                .IsRequired();
        }
    }
}
