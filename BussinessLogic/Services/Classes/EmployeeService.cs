using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLogic.DTOs.EmployeeDTOs;
using BussinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModels;
using DataAccess.Models.SharedModels;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;

namespace BussinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository , IMapper _mapper) /*//To connect to the dbcontext)*/ : IEmployeeService
    {




        public IEnumerable<GetEmployeeDto> GetAllEmployees()
        {

            var employees = _employeeRepository.GetAll();
            //Manual Mapping
            //Map From Ienumerable <Employee> to IEnumerable <GetEmployeeDto>
            //var employeesDto = employees.Select(emp => new GetEmployeeDto()
            //{
            //    Id = emp.Id,
            //    Name = emp.Name,
            //    Age = emp.Age,
            //    Salary = emp.Salary,
            //    Email = emp.Email,
            //    IsActive = emp.IsActive,
            //    EmployeeType = emp.EmployeeType.ToString(),
            //    Gender = emp.Gender.ToString(),
            //});

            //Auto Mapper 
            //Map From Ienumerable <Employee> to IEnumerable <GetEmployeeDto>
            //return TDestination (Mapped Object)
            //IMapper.Map<TDestination> (TSource)
            //IMapper.Map<TSource , TDestination> (TSource)

            var employeesDto = _mapper.Map<IEnumerable<GetEmployeeDto>>(employees);
            return employeesDto;

            //IEnumerable vs IQueryable
            //var employees = _employeeRepository.GetAll(e => new GetEmployeeDto()
            //{
            //    Id = e.Id,
            //    Name = e.Name,
            //    Age = e.Age,
            //});
            //return employees;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var emp=_employeeRepository.GetById(id) ;
            return emp is null ? null: _mapper.Map<EmployeeDetailsDto>(emp); 

        }

        #region Create Employee

        public int CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(createEmployeeDto);
            var res=_employeeRepository.Add(mappedEmployee);
            return res;
        } 
        #endregion
        public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
           var mappedEmployee =_mapper.Map<Employee>(updateEmployeeDto);
            var res= _employeeRepository.Update(mappedEmployee);
            return res;
        }
        public bool DeleteEmployee(int id)
        {
            var emp= _employeeRepository.GetById(id);  
            if (emp is null) return false;
            else
            {//Soft Delete
                emp.IsDeleted= true;
                var res=_employeeRepository.Update(emp);
                return res>0 ?true : false;
            }
        }
    }
}
