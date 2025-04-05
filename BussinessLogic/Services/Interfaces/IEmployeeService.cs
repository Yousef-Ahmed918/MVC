using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BussinessLogic.DTOs.DepartmentDTOs;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.EmployeeDTOs;

namespace BussinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        int CreateEmployee(CreateEmployeeDto createEmployeeDto);
        IEnumerable<GetEmployeeDto> GetAllEmployees();
        EmployeeDetailsDto? GetEmployeeById(int id);
        int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
        bool DeleteEmployee(int id);
    }
}
