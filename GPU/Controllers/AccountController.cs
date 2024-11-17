using GPU.Helpers;
using GPU.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using GPU.Services;
using System.Data;

namespace GPU.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View(new UsersModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsersModel model)
        {
            var usr = ManagerServices._Users.FirstOrDefault(x => x.UserName.ToLower() == model.UserName.ToLower() && x.Password == model.Password);

            if (usr != null)
            {

                if (ManagerServices._employees.Any(x => (x.id == usr.EMPID) && (x.StuList || x.ArchList)))
                {
                    var emp = ManagerServices._employees.FirstOrDefault(x => (x.id == usr.EMPID) && (x.StuList || x.ArchList));
                    if (ManagerServices._auths.Any(x => x.usrid == usr.id))
                    {
                        var employee = ManagerServices._employees.FirstOrDefault(x => x.id == usr.EMPID);

                        var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, emp.FullName),
                    new Claim(ClaimTypes.GivenName, usr.UserName),
                    new Claim(ClaimTypes.NameIdentifier, usr.id.ToString())};

                        // Create a claims identity

                        // Set the authentication properties (with IsPersistent set to false)
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = false, // This makes the cookie a session cookie (non-persistent)
                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30) // Optional: Add a time span for cookie expiration
                        };
                        if (employee.StuList)
                        {
                            claims.Add(new Claim("Permission", "StuList"));
                        }

                        if (employee.ArchList)
                        {
                            claims.Add(new Claim("Permission", "ArchList"));
                        }
                        var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");

                        // Sign in the user by creating the cookie with the specified properties
                        await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);


                        return RedirectToAction("Index", "Home");
                    }
                    return BadRequest();
                }

                return BadRequest();

            }
            else
            {
                model.isWrongPass = true;
                return View("Login", model);
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("CookieAuth"); // Use "CookieAuth" here, as registered in Startup/Program.cs
            return RedirectToAction("Login", "Account");
        }

    }
}
