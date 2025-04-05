using System.ComponentModel.DataAnnotations;

namespace MVC.View_Models.DepartmentViewModels
{
    public class DepartmentViewModels
    {
        [Required(ErrorMessage = "Name Is Required")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
    }
}

