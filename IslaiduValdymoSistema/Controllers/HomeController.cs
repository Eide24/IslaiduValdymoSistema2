using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using IslaiduValdymoSistema.Models;
namespace IslaiduValdymoSistema.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
