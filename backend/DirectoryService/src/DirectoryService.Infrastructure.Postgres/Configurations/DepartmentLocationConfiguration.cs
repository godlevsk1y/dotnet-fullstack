using DirectoryService.Domain.Ids;
using DirectoryService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DirectoryService.Infrastructure.Postgres.Configurations;

public class DepartmentLocationConfiguration : IEntityTypeConfiguration<DepartmentLocation>
{
    public void Configure(EntityTypeBuilder<DepartmentLocation> builder)
    {
        builder.ToTable("department_locations");
        
        builder.HasKey(dl => dl.Id)
            .HasName("pk_department_locations");

        builder.Property(dl => dl.Id)
            .HasColumnName("id");

        builder.Property(dl => dl.DepartmentId)
            .IsRequired()
            .HasConversion(dl => dl.Value, id => new DepartmentId(id))
            .HasColumnName("department_id");
        
        builder.Property(dl => dl.LocationId)
            .IsRequired()
            .HasConversion(dl => dl.Value, id => new LocationId(id))
            .HasColumnName("location_id");
        
        builder.HasOne<Department>()
            .WithMany()
            .HasForeignKey(dl => dl.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_department_locations_departments_department_id");
        
        builder.HasOne<Location>()
            .WithMany()
            .HasForeignKey(dl => dl.LocationId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_department_locations_locations_location_id");
    }
}