using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Demo.Restuarants.Infrastructure.MSSQL.Entities;

[EntityTypeConfiguration(typeof(RestuarantEntityConfiguration))]
public class RestuarantEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string CuisineType { get; set; } = string.Empty;
    public string? Website { get; set; }
    public string Phone { get; set; } = string.Empty;

    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;
}

public class RestuarantEntityConfiguration : IEntityTypeConfiguration<RestuarantEntity>
{
    public void Configure(EntityTypeBuilder<RestuarantEntity> builder)
    {
        builder.ToTable("Restuarant");

        // TODO: Any way to move this to a base class?
        builder.HasKey(x => x.Id);
        builder.Property(_ => _.Id).IsRequired().HasMaxLength(50);

        builder.Property(_ => _.Name).IsRequired().HasMaxLength(50);
        builder.Property(_ => _.CuisineType).IsRequired().HasMaxLength(30);
        builder.Property(_ => _.Website).HasMaxLength(100);
        builder.Property(_ => _.Phone).HasMaxLength(20);

        builder.Property(_ => _.Street).IsRequired().HasMaxLength(150);
        builder.Property(_ => _.City).IsRequired().HasMaxLength(100);
        builder.Property(_ => _.State).IsRequired().HasMaxLength(2);
        builder.Property(_ => _.Country).IsRequired().HasMaxLength(100);
        builder.Property(_ => _.ZipCode).IsRequired().HasMaxLength(10);
    }
}