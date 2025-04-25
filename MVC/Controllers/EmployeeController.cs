using BussinessLogic.DTOs.DepartmentDTOs;
using BussinessLogic.DTOs.EmployeeDTOs;
using BussinessLogic.Services.Classes;
using BussinessLogic.Services.Interfaces;
using DataAccess.Models.EmployeeModels;
using DataAccess.Models.SharedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.View_Models.DepartmentViewModels;
using MVC.View_Models.EmployeeViewModels;

namespace MVC.Controllers
{
    [Authorize]
    public class EmployeeController(IEmployeeService _employeeService
         , ILogger<EmployeeController> _logger,
        IWebHostEnvironment _environment
        ) : Controller
       

    {
        public IActionResult Index(string? EmployeeSearchName)
        {
            var employees=_employeeService.GetAllEmployees(EmployeeSearchName);
            return View(employees);
        }
 
        #region Create
        [HttpGet]
        public IActionResult Create(/*[FromServices]IDepartmentServices _departmentServices //ActionInjection*/)
        {
            //ViewData["Departments"]=_departmentServices.GetAllDepartments();
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModels employeeView)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createEmployeeDto = new CreateEmployeeDto()
                    {
                        Name = employeeView.Name,
                        Address = employeeView.Address,
                        Age = employeeView.Age,
                        DeptId = employeeView.DeptId,
                        Email = employeeView.Email,
                        Gender = employeeView.Gender,
                        EmployeeType = employeeView.EmployeeType,
                        HiringDate = employeeView.HiringDate,
                        IsActive = employeeView.IsActive,
                        PhoneNumber = employeeView.PhoneNumber,
                        Salary = employeeView.Salary,
                        Image=employeeView.Image
                    }; 
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
            return View(employeeView);
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
        public IActionResult Edit(int? id/*,[FromServices]IDepartmentServices _departmentServices //Action Injection*/)
        {
            if (!id.HasValue) return BadRequest();
            var employee=_employeeService.GetEmployeeById(id.Value);
            //Map From EmployeeDetailsDto to UpdateEmployeeDto
            //ViewData["Departments"] = _departmentServices.GetAllDepartments();
            var employeeViewModels = new EmployeeViewModels()
            {
                Name = employee.Name,
                Address = employee.Address,
                Age = employee.Age,
                Email = employee.Email,
                HiringDate = employee.HiringDate,
                PhoneNumber = employee.PhoneNumber,
                IsActive = employee.IsActive,
                Salary = employee.Salary,
                Gender = Enum.Parse<Gender>(employee.EmpGender),
                EmployeeType = Enum.Parse<EmployeeType>(employee.EmpType),
                DeptId=employee.DeptId
            };
            return View(employeeViewModels);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModels employeeView)
        { 

            if (!id.HasValue ) return BadRequest();
            if (!ModelState.IsValid) return View(employeeView);

            try
            {

                var employeeViewModels = new UpdateEmployeeDto()
                {
                    Id=id.Value,
                    Name = employeeView.Name,
                    Address = employeeView.Address,
                    Age = employeeView.Age,
                    Email = employeeView.Email,
                    HiringDate = employeeView.HiringDate,
                    PhoneNumber = employeeView.PhoneNumber,
                    IsActive = employeeView.IsActive,
                    Salary = employeeView.Salary,
                    Gender = employeeView.Gender,
                    EmployeeType = employeeView.EmployeeType,
                    DeptId = employeeView.DeptId
                };
                var res = _employeeService.UpdateEmployee(employeeViewModels);
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
            return View(employeeView);
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
