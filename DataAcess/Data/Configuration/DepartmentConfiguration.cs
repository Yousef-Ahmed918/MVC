﻿



using DataAccess.Data.Configuration;
using DataAccess.Models.DepartmentModels;

namespace DataAcess.Data.Configuration
{
    internal class DepartmentConfiguration : BaseEntityConfiguration<Department>,IEntityTypeConfiguration<Department>
    { 
        public new void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(dept=>dept.Id).UseIdentityColumn(10,10);
            builder.Property(dept => dept.Name).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Code).HasColumnType("varchar(20)");
            builder.HasMany(dept => dept.Employees)
                .WithOne(emp=>emp.Department)
                .HasForeignKey(emp=>emp.DeptId)
                .OnDelete(DeleteBehavior.SetNull);
            base.Configure(builder);
        }
    }
}
 