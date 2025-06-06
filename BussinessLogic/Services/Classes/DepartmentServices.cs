﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.DepartmentDTOs;
using BussinessLogic.Factory;
using BussinessLogic.Services.Interfaces;
using DataAccess.Repositories.Interfaces;

namespace BussinessLogic.Services.Classes
{
    public class DepartmentServices(IUnitOfWork _unitOfWork) : IDepartmentServices
    {
        //Get All Departments 
        public IEnumerable<GetDepartmentDto> GetAllDepartments()
        {
            //All this to avoid dbContext.Departments
            var departments = _unitOfWork.DepartmentRepository.GetAll();
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
            var department = _unitOfWork.DepartmentRepository.GetById(id);
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
           _unitOfWork.DepartmentRepository.Add(createDepartmentDto.ToEntity());
            return _unitOfWork.SaveChanges();
        }

        //Update Department 
        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var dept = updateDepartmentDto.ToEntity();
            _unitOfWork.DepartmentRepository.Update(dept);
            return _unitOfWork.SaveChanges();
        }

        //Delete Department
        public bool DeleteDepartment(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                _unitOfWork.DepartmentRepository.Remove(department);
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }


    }
}
