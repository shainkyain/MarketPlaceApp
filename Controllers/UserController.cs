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
            var role = HttpContext.Session.GetString("LoggedUserRole");
            if(role != "Admin")
            {
                return RedirectToAction("SomethingWentWrong", "Home");
            }
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
            TempData["Message"] = "Enter Valid Credentials";
            return RedirectToAction("SignIn", "User");
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult CreateUser(User user)
        {
            if (user.mobile.Length == 10 && user.password.Length >= 8 && user.Name.Length >= 3)
            {

                userService.AddUser(user);
                return RedirectToAction("SignIn", "User");

            }
            TempData["Message"] = "Something Went Wrong Please ReEnter Correct values to each Input";
            return RedirectToAction("SignUp", "User");
        }

        public IActionResult ChangeRoleFunction(int id)
        {
           User user =  userService.GetAllUsers().FirstOrDefault(user => user.Id == id);
            userService.ChangeRole(user);
            return RedirectToAction("AllUserList", "User");
        }












        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("SignIn", "User");
        }


        

    }
}
