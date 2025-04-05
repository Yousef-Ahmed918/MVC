using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.DTOs.DepartmentDTOs
{
    public class DepartmentDtoBase
    {
        public int DeptId { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DateOfCreation { get; set; }
    }
}
