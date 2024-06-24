using Domain.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace СomputerСlub.Persistence.EfModels
{
    public class EfModelPerson : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(p => p.IdClient);

            builder.Property(p => p.Name)
              .HasMaxLength(20);

            builder.Property(p => p.Surname)
              .HasMaxLength(40)
              .IsRequired();
            builder.Property(p => p.Patronymic)
              .HasMaxLength(20)
              .IsRequired();

            builder.Property(p => p.NumberOfHours)
              .HasColumnType("INTEGER")
              .IsRequired();
        }
    }
}
