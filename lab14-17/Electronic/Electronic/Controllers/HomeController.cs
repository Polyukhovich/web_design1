using Electronic.Data;
using Electronic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Electronic.Controllers
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
            var devices = _dbContext.Devices
            .Include(b => b.Parameter)
                .ToList();
            var parameters = _dbContext.Parameters
                .Include(a => a.Devices)
                .ToList();
            ViewBag.Parameters = parameters;
            return View(devices);
        }

        public IActionResult Create()
        {
            ViewBag.Parameters = _dbContext.Parameters.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Device devices)
        {
            if (devices != null)
            {
                _dbContext.Devices.Add(devices);
                _dbContext.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateParameter(int id )

        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateParameter( Parameter parameter)
        {
            if(parameter != null)
            {
                _dbContext.Parameters.Add(parameter);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // якщо модель нед≥йсна, поверн≥ть користувача на стор≥нку створенн€ параметра з пов≥домленн€ми про помилки
            return View(parameter);
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
            _dbContext.Purchases.Add(purchase);
            _dbContext.SaveChanges();
            return "ƒ€кую ," + purchase.Person + "за покупку";
        }
        public IActionResult ChooseDeviceForEdit()
        {
            var devices = _dbContext.Devices.ToList();
            return View(devices);
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
