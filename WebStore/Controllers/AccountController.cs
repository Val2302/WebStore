using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Models.Account;

namespace WebStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login() => View(new LoginViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                var userName = userModel.EmailOrUserName;

                // Сначала проверяем является ли введенная пользователем строка электронным адресом
                if (userName.IndexOf('@') > -1)
                {
                    var user =  await _userManager.FindByEmailAsync(userModel.EmailOrUserName);
                    if (user is null)
                    {
                        ModelState.AddModelError(String.Empty, "Invalid login arguments" );
                        return View(userModel);
                    }
                    // Если является, то перезаписываем userName на значение, полученное из БД
                    else
                        userName = user.UserName;
                }

                var loginResult = await _signInManager.PasswordSignInAsync(userName,
                    userModel.Password,
                    userModel.RememberMe,
                    lockoutOnFailure: false);

                if (loginResult.Succeeded)
                {
                    if (Url.IsLocalUrl(userModel.ReturnUrl))
                        return Redirect(userModel.ReturnUrl);

                    return RedirectToAction(controllerName: "Home", actionName: "Index");
                }
            }

            ModelState.AddModelError(String.Empty, "Can not login");

            return View(userModel);
        }

        [HttpGet]
        public IActionResult Register() => View(new RegisterViewModel());
        
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Email = model.Email,
                    UserName = model.UserName
                };

                if (user.UserName is null || user.UserName == String.Empty)
                    user.UserName = new string(user.Email.TakeWhile(s => s != '@').ToArray());

                var createResult = await _userManager.CreateAsync(user, model.Password);

                if (createResult.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction(controllerName: "Home", actionName: "Index");
                }

                foreach (var identityError in createResult.Errors)
                    ModelState.AddModelError("", identityError.Description);
            }

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }
    }
}