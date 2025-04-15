using BussinessLogic.DTOs.DepartmentDTOs;
using BussinessLogic.DTOs.EmployeeDTOs;
using BussinessLogic.Services.Classes;
using BussinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModels;
using DataAccess.Models.SharedModels;
using Microsoft.AspNetCore.Mvc;
using MVC.View_Models.DepartmentViewModels;

namespace MVC.Controllers
{
    public class EmployeeController(IEmployeeService _employeeService
         , ILogger<EmployeeController> _logger,
        IWebHostEnvironment _environment
        ) : Controller
       

    {
        public IActionResult Index()
        {
            var employees=_employeeService.GetAllEmployees();
            return View(employees);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = _employeeService.CreateEmployee(createEmployeeDto);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Cant Create This Employee");
                    }

                }
                catch (Exception ex)
                {
                    if (_environment.IsDevelopment())
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                    else
                    {
                        _logger.LogError(ex.Message);
                    }
                }

            }
            return View(createEmployeeDto);
        }
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int ? id)
        {
            if (!id.HasValue) BadRequest();
            var employee = _employeeService.GetEmployeeById(id.Value);
            return employee is null?NotFound():View(employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee=_employeeService.GetEmployeeById(id.Value);
            //Map From EmployeeDetailsDto to UpdateEmployeeDto
            var updatedEmployeeDto = new UpdateEmployeeDto()
            {
                Id = id.Value,
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Gender = Enum.Parse<Gender>(employee.EmpGender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmpType)
            };
            return View(updatedEmployeeDto);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, UpdateEmployeeDto updateEmployeeDto)
        { 

            if (!id.HasValue||id != updateEmployeeDto.Id) return BadRequest();
            if (!ModelState.IsValid) return View(updateEmployeeDto);

            try
            {

               
                var res = _employeeService.UpdateEmployee(updateEmployeeDto);
                if (res > 0) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "Employee Cant Be Updated");
                    //return View(departmentViewModels);
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment())
                {
                    //1)Development  => Console 
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                else
                {
                    _logger.LogError(ex.Message);
                }

            }
            return View(updateEmployeeDto);
        }

        #endregion

        #region Delete
        [HttpPost]
        public IActionResult Delete(int id) {
            if (id ==0) return BadRequest();
            try
            {
                var IsDeleted = _employeeService.DeleteEmployee(id);
                if (IsDeleted) return RedirectToAction(nameof(Index));
                else
                {
                    ModelState.AddModelError(string.Empty, "This Employee Cant Be Deleted");
                    return RedirectToAction(nameof(Delete) , new {id});
                }
            }
            catch (Exception ex)
            {
                if (_environment.IsDevelopment()) ModelState.AddModelError(string.Empty, ex.Message);
                _logger.LogError(ex.Message);
            }
            return View("Error");
        }
        #endregion

    }
}
