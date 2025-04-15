using BussinessLogic.DTOs.EmployeeDTOs;
using BussinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

    }
}
