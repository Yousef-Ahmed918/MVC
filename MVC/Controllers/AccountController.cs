using DataAccess.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Helper;
using MVC.View_Models.AccountViewModels;

namespace MVC.Controllers
{
    public class AccountController(UserManager<ApplicationUser> _userManager , 
        SignInManager <ApplicationUser> _signInManager) : Controller
    {
        //To handle to authorization and doesnt allow anonymous 
        //[Authorize]

        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerView)
        {
            if (ModelState.IsValid)
            {
                //Map from register view model to application user
                var user = new ApplicationUser()
                {
                    FirstName = registerView.FirstName,
                    LastName = registerView.LastName,
                    Email = registerView.Email,
                    UserName = registerView.UserName
                };
                var result = _userManager.CreateAsync(user, registerView.Password).Result;
                if (result.Succeeded) //Finished registeration then redirect to Login
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
            }
            return View(registerView);
        }
        #endregion

        #region Log In
        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogIn(LogInViewModel logInView)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByEmailAsync(logInView.Email).Result;
                if (user is null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid LogIn");

                }
                else
                {
                    var flag = _userManager.CheckPasswordAsync(user, logInView.Password).Result;
                    if (flag)
                    {
                        var result = _signInManager.PasswordSignInAsync(user, logInView.Password, false, false).Result;
                        if (result.Succeeded) return RedirectToAction("Index", "Home");
                    }
                    else ModelState.AddModelError(string.Empty, "Invalid LogIn");
                }

            }
            return View(logInView);
        }
        #endregion

        #region Log Out
        public IActionResult LogOut()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction(nameof(LogIn));
        }
        #endregion

        #region Forget Password
        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendResetPasswordUrl(ForgetPasswordViewModel  viewModel)
        {
            if (ModelState.IsValid)
            {
                var user=_userManager.FindByEmailAsync(viewModel.Email).Result;
                if (user is not null)
                {

                    //Generate Token
                    var token=_userManager.GeneratePasswordResetTokenAsync(user).Result;
                    
                    //Create URL
                    //Its property in the Base Controller
                                                                                                   //Token   //return the base url
                    var url = Url.Action("ResetPassword", "Account", new { email = viewModel.Email ,token },Request.Scheme);

                    //Create Email
                    var email = new Email()
                    {
                        To = viewModel.Email,
                        Subject = "Reset Password",
                        Body = url
                    };
                    //Send Email
                 bool isMailSent=EmailSetting.SendEmail(email);
                    if (isMailSent)
                    {
                        return RedirectToAction(nameof(CheckYourInbox));
                    }
                }
                ModelState.AddModelError(string.Empty, "Something went Wrong , Try Again");
            }
            return View(nameof(ForgetPassword));
        }

        public IActionResult CheckYourInbox()
        {
            return View();
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public  IActionResult ResetPassword(string email,string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public IActionResult ResetPassword(ResetPasswordViewModel viewModel)
        {
            //use Temp data to bind the token and the email when move from the get request to the post request
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;
                if (email is null || token is null) return BadRequest();
                else
                {
                    var user=_userManager.FindByEmailAsync(email).Result;
                    if (user is not null)
                    {
                        var result = _userManager.ResetPasswordAsync(user, token, viewModel.Password).Result;
                        if (result.Succeeded) return RedirectToAction(nameof(LogIn));
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error!");
                    }
                }
            }
            return View(viewModel);
        }
        #endregion

    }


}
