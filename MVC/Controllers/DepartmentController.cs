using Microsoft.AspNetCore.Mvc;
using BussinessLogic.Services;
using BussinessLogic.DTOs;
using MVC.View_Models.DepartmentViewModels;

namespace MVC.Controllers
{
    //Constructor injection
    public class DepartmentController(IDepartmentServices _departmentService,
        ILogger<DepartmentController> _logger,
        IWebHostEnvironment _environment
        ) : Controller
    {
        public IActionResult Index()
        {
            var departments=_departmentService.GetAllDepartments();
            return View(departments);
        }

        #region Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentDto createDepartmentDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int res=_departmentService.CreateDepartment(createDepartmentDto);
                    if (res>0)  return RedirectToAction(nameof(Index)); //Back To list
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Department Cant Be Created");
                    }
                }
                catch (Exception ex)
                {
                    //Log Exception 
                    //1)Development  => Console 
                    //2)Deployment   => File / Table In the Database (More organaized)
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
            }
            
                return View(createDepartmentDto);
           
        } 
        #endregion

        //Details
     public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null) return NotFound();
            return View(department);
        }

        //Edit 
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _departmentService.GetDepartmentById(id.Value);

            //Map From DepartmentDetailsDto  To DepartmentEditViewModel
            if (department is null) return NotFound();
            else
            {
                var departmentViewModel = new DepartmentViewModels()
                {
                    Name = department.Name,
                    Code = department.Code,
                    Description = department.Description,
                    DateOfCreation = department.DateOfCreation
                };
            return View(departmentViewModel);
            }
        }
        [HttpPost]
        public IActionResult Edit([FromRoute]int  id,DepartmentViewModels departmentViewModels)
        {
            if (!ModelState.IsValid) return View(departmentViewModels);
           
                try
                {
                    var UpdatedDept = new UpdateDepartmentDto()
                    {
                        DeptId=id,
                        Code = departmentViewModels.Code, 
                        Name = departmentViewModels.Name, 
                        Description = departmentViewModels.Description,
                        DateOfCreation = departmentViewModels.DateOfCreation
                    };
                    var res = _departmentService.UpdateDepartmetn(UpdatedDept);
                    if (res > 0) return RedirectToAction(nameof(Index));
                    else 
                    {
                        ModelState.AddModelError(string.Empty, "Department Cant Be Updated");
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
            return View(departmentViewModels);
        }

    }
}
