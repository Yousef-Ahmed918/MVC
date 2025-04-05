using BussinessLogic.DTOs;

namespace BussinessLogic.Services
{
    public interface IDepartmentServices
    {
        int CreateDepartment(CreateDepartmentDto createDepartmentDto);
        IEnumerable<DepartmentDto> GetAllDepartments();
        DepartmentsDetailsDto? GetDepartmentById(int id);
        int UpdateDepartmetn(UpdateDepartmentDto updateDepartmentDto);
    }
}