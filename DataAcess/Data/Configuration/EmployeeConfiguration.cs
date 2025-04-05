using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.EmployeeModels;
using DataAccess.Models.SharedModels;

namespace DataAccess.Data.Configuration
{
    internal class EmployeeConfiguration :BaseEntityConfiguration<Employee> ,IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e=>e.Name).HasColumnType("varchar(50)"); 
            builder.Property(e=>e.Address).HasColumnType("varchar(150)"); 
            builder.Property(e=>e.Salary).HasColumnType("decimal(10,2)"); 
            builder.Property(e=>e.Email).HasColumnType("varchar(30)"); 
            builder.Property(e=>e.PhoneNumber).HasColumnType("varchar(11)");

            //Enum configuration
            builder.Property(e => e.EmployeeType).HasConversion(
                convertToProviderExpression: valueToAddInDb => valueToAddInDb.ToString(),
                convertFromProviderExpression: valueToReadFromDb => (EmployeeType)Enum.Parse(typeof(EmployeeType), valueToReadFromDb))
                .HasColumnType("varchar(8)");
        
            builder.Property(e=>e.Gender).HasConversion(
                convertToProviderExpression: ValueToAddInDb =>ValueToAddInDb.ToString(),
                convertFromProviderExpression: ValueToReadFromDb =>(Gender)Enum.Parse(typeof(Gender), ValueToReadFromDb))
                .HasColumnType("varchar(6)") ;
        base.Configure(builder);
        }
    }
}
