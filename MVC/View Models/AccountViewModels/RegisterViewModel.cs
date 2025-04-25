using System.ComponentModel.DataAnnotations;

namespace MVC.View_Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="First Name Cant Be Empty")]
        [MaxLength(50)]
        public string FirstName { get; set; } = null!;
        [Required(ErrorMessage = "First Name Cant Be Empty")]
        [MaxLength(50)]
        public string LastName { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }=null!;

        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        public bool IsAgree { get; set; }


    }
}
