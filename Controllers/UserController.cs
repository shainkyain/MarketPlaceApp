using MarketPlaceApp.Models;
using MarketPlaceApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlaceApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService userService = new UserService();

        public IActionResult AllUserList()
        {
            List<User> allUsers = userService.GetAllUsers();
            return View(allUsers);
        }

        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult Auth(User user)
        {
            User u = userService.GetAllUsers().FirstOrDefault(u => u.Email.Equals(user.Email) && u.password.Equals(user.password));
            
            if(u != null)
            {
                HttpContext.Session.SetString("LoggedUserEmail", u.Email);
                HttpContext.Session.SetString("LoggedUserRole", u.Role);

                //ViewData["Role"] = HttpContext.Session.GetString("LoggedUserRole");
                    return RedirectToAction("AllProductList", "Product");
                
                 
            }

            return RedirectToAction("SignIn", "User");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult CreateUser(User user)
        {
            userService.AddUser(user);
            return RedirectToAction("SignIn", "User");
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "User");
        }

        

    }
}
