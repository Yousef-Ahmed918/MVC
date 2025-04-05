using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.DTOs;
using BussinessLogic.Factory;
using DataAcess.Repositories;

namespace BussinessLogic.Services
{
    public class DepartmentServices(IDepartmentRepository _departmentRepository) : IDepartmentServices
    {
        //Get All Departments 
        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            //All this to avoid dbContext.Departments
            var departments = _departmentRepository.GetAll();
            //var DepartmentToReturn = departments.Select(d => new DepartmentDto()
            //{
            //    DeptId = d.Id,
            //    Name = d.Name,
            //    Code = d.Code,
            //    Description = d.Description,
            //    DateOfCreation = d.CreatedOn
            //});
            //OR Mapping Extenstion
            var DepartmentToReturn = departments.Select(d => d.ToDepartmentDto());
            return DepartmentToReturn;
        }

        //Get Departments by Id
        public DepartmentsDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);
            //if (department is null) return null;
            //else
            //{
            //    var DepartmentToReturn = new DepartmentsDetailsDto()
            //    {
            //        DeptId = department.Id,
            //        Name = department.Name,
            //        Code = department.Code,
            //        Description = department.Description,
            //        DateOfCreation = department.CreatedOn,
            //        CreatedBy = department.CreatedBy,
            //        LastModifiedBy = department.LastModifiedBy,
            //        IsDeleted = department.IsDeleted

            //    };
            //    return DepartmentToReturn;
            //}

            //OR

            //Manual Mapping
            //return department is null? null : new DepartmentsDetailsDto()
            //{
            //    DeptId = department.Id,
            //    Name = department.Name,
            //    Code = department.Code,
            //    Description = department.Description,
            //    DateOfCreation = department.CreatedOn, 
            //    CreatedBy = department.CreatedBy,
            //    LastModifiedBy = department.LastModifiedBy,
            //    IsDeleted = department.IsDeleted
            //};

            //OR
            //Extension Mapping
            return department is null ? null : department.ToDepartmentDetailsDto();

            //Types Of Mapping
            //1)Manual Mapping
            //2)Auto Mapper
            //3)Constructor Mapping(Considered a Better Version Of The Manual Mapping)
            //4)Extension Method(Considered a Better Version Of The Manual Mapping)

        }

        //Create Department
        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var result = _departmentRepository.Add(createDepartmentDto.ToEntity());
            return result;
        }

        //Update Department 
        public int UpdateDepartmetn(UpdateDepartmentDto updateDepartmentDto)
        {
            var dept = updateDepartmentDto.ToEntity();
            var resutl = _departmentRepository.Update(dept);
            return resutl;
        }


    }
}
