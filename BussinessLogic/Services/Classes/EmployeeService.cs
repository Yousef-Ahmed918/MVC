using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BussinessLogic.DTOs.EmployeeDTOs;
using BussinessLogic.Services.AttachmentServices;
using BussinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModels;
using DataAccess.Models.SharedModels;
using DataAccess.Repositories.Interfaces;
using Microsoft.Extensions.Logging.Abstractions;

namespace BussinessLogic.Services.Classes
{
    public class EmployeeService(IUnitOfWork _unitOfWork 
        , IMapper _mapper
        ,IAttechmentService attechmentService
        ) /*//To connect to the dbcontext)*/ : IEmployeeService
    {
        private readonly IAttechmentService _attechmentService = attechmentService;

        public IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
            {

                employees = _unitOfWork.EmployeeRepository.GetAll();
            }
            else
            {
            //.ToLower() To avoid case sensetivity problem 
            //Contains to get the name without the need to input the full name
             employees = _unitOfWork.EmployeeRepository.GetAll(e=> e.Name.ToLower()
                                                    .Contains (EmployeeSearchName.ToLower()));

            }
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
            var emp= _unitOfWork.EmployeeRepository.GetById(id) ;
            return emp is null ? null: _mapper.Map<EmployeeDetailsDto>(emp); 

        }

        #region Create Employee

        public int CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            //Call Attachment Service to Upload Employee Image
            var mappedEmployee = _mapper.Map<Employee>(createEmployeeDto);
            var imageName = _attechmentService.Upload(createEmployeeDto.Image,"Images");
            mappedEmployee.ImageName = imageName; 
            _unitOfWork.EmployeeRepository.Add(mappedEmployee);
            return _unitOfWork.SaveChanges();
        } 
        #endregion
        public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
           var mappedEmployee =_mapper.Map<Employee>(updateEmployeeDto);
            _unitOfWork.EmployeeRepository.Update(mappedEmployee);
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var emp = _unitOfWork.EmployeeRepository.GetById(id);  
            if (emp is null) return false;
            else
            {//Soft Delete
                emp.IsDeleted= true;
                _unitOfWork.EmployeeRepository.Update(emp);
            return _unitOfWork.SaveChanges() >0 ? true :false ;
            }
        }

        
    }
}
