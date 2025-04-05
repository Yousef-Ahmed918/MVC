using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Core.Pipeline;
using DataAccess.Models.DepartmentModels;

namespace BussinessLogic.DTOs.DepartmentDTOs
{
    public class DepartmentsDetailsDto : DepartmentDtoBase
    {
        public DepartmentsDetailsDto()
        {

        }
        //Mapping Constructor
        public DepartmentsDetailsDto(Department department)
        {
            DeptId = department.Id;
            Name = department.Name;
            Code = department.Code;
            Description = department.Description;
            DateOfCreation = department.CreatedOn;
            CreatedBy = department.CreatedBy;
            LastModifiedBy = department.LastModifiedBy;
            LastModifiedOn = department.LastModifiedOn;
            IsDeleted = department.IsDeleted;
        }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }

    }
}
