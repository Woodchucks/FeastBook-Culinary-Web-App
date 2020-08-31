using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using FeastBook_final.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FeastBook_final.Data;
using Microsoft.AspNetCore.Http;

namespace FeastBook_final.Controllers
{
    public class HomeController : Controller
    {
        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var applicationDbContext = _context.Kategorie.Include(k => k.NadKategoria);

            var kategorie = from k in _context.Kategorie
                           select k;

            return View(kategorie);
        }
        [AllowAnonymous]
        public IActionResult Main()
        {
            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
