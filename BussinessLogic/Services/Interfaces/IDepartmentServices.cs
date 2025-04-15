using BussinessLogic.DTOs;
using BussinessLogic.DTOs.DepartmentDTOs;

namespace BussinessLogic.Services.Interfaces
{
    public interface IDepartmentServices
    {
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<GetDepartmentDto> GetAllDepartments();
        DepartmentsDetailsDto? GetDepartmentById(int id);
        int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto);
        bool DeleteDepartment(int id);

    }
}