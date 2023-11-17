using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Electronics> machinery = _dbContext.Machinery;
            ViewBag.Machinery = machinery;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Create(Electronics machinery)
        {
            if (machinery != null)
            {
                _dbContext.Machinery.Add(machinery);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult Buy(int? Id) 
        {
            ViewBag.ElectronicsId = Id ?? 0;
            return View();
        }

        [HttpPost]
        public string Buy(Purchase purchase)
        {
                purchase.Date = DateTime.Now;
            _dbContext.Purchase.Add(purchase);
            _dbContext.SaveChanges();
            return "ƒ€куЇмо " + purchase.Person + " за куп≥влю!";
        }

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
