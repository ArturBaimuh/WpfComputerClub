using Domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace СomputerСlub.Persistence.EfModels
{
    class EfModelGameRoom : IEntityTypeConfiguration<Room>
    {
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.HasKey(p => p.IdHall);

        }
    }
}
