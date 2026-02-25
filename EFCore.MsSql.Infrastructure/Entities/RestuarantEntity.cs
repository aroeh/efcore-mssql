using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCore.MSSQL.Infrastructure.Entities;

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
        builder.HasKey(x => x.Id);
    }
}