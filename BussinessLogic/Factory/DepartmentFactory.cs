﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.DepartmentDTOs;
using DataAccess.Models.DepartmentModels;


namespace BussinessLogic.Factory
{
    public static class DepartmentFactory
    {//Mapping Extension Method

       
        public static GetDepartmentDto ToDepartmentDto(this Department department)
        {
            return new GetDepartmentDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };
        }

        public static DepartmentsDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentsDetailsDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
                CreatedBy = department.CreatedBy,
                LastModifiedBy = department.LastModifiedBy,
                IsDeleted = department.IsDeleted

            };
        }
    
    public static Department ToEntity (this CreateDepartmentDto dto)
        {
            return new Department
            {
                Id = dto.DeptId,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation
            };
        }
        public static Department ToEntity (this UpdateDepartmentDto dto)
        {
            return new Department
            {
                Id=dto.DeptId,
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                CreatedOn = dto.DateOfCreation
            };
        }
        
    }
}
