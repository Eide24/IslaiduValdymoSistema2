using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IslaiduValdymoSistema.Models;

namespace IslaiduValdymoSistema.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
           
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }

            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
