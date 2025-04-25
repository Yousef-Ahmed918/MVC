using System.ComponentModel.DataAnnotations;

namespace MVC.View_Models.AccountViewModels
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
    }
}
