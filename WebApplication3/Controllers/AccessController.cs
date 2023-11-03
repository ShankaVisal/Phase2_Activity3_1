using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebApplication3.Models;

namespace WebApplication3.Controllers;

public class AccessController: Controller
{
    public IActionResult Login()
    {
        ClaimsPrincipal claimUser = HttpContext.User;
        if (claimUser.Identity.IsAuthenticated)
            return RedirectToAction("Index", "Home");
        
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(VMLogin modelLogin)
    {
        if(modelLogin.Email == "shankavisal@gmail.com" && modelLogin.Password == "123"){
        
            var claims = new List<Claim>
            {
            new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
            new Claim("OtherProperties", "Example Role")
        };
            ClaimsIdentity ClaimsIdentity = new ClaimsIdentity(claims, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = true
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(ClaimsIdentity),properties);

            return RedirectToAction("Index", "Home");
        }

        ViewData["ValidateMessage"] = "user not Found";
        return View();
    }
}