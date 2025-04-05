using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models.SharedModels;

namespace DataAccess.Data.Configuration
{
    internal class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
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
