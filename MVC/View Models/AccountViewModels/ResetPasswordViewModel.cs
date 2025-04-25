using System.ComponentModel.DataAnnotations;

namespace MVC.View_Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
