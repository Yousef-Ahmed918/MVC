



namespace DataAcess.Data.Configuration
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    { 
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(dept=>dept.Id).UseIdentityColumn(10,10);
            builder.Property(dept => dept.Name).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Code).HasColumnType("varchar(20)");

            //Computed properities
            builder.Property(dept => dept.CreatedOn).HasDefaultValueSql("GETDATE()"); 
            //if row is inserted without value , the defalut value will be used 
            //Used on Insert 
            //Can not refernces other columns
            //Can be overriden
            builder.Property(dept => dept.LastModifiedOn).HasComputedColumnSql("GETDATE()"); //set default value when triggerd in sql
            //value is computed every time the record changed 
            //Used on Update 
            //Can refernces other columns 
            //Can not  be overriden
        }
    }
}
 