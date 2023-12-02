using Electronick.Data;
using Electronick.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Electronick.Controllers
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
        // Додавання девайсу
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
        //Додавання виробника
        public IActionResult CreateParameter(int id)

        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateParameter(Parameter parameter)
        {
            if (parameter != null)
            {
                _dbContext.Parameters.Add(parameter);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Якщо модель недійсна, поверніть користувача на сторінку створення параметра з повідомленнями про помилки
            return View(parameter);
        }
        [HttpGet]
        //покупка товару
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
            return "Дякую ," + purchase.Person + "за покупку";
        }
        //Видалення виробника
        public ActionResult DeleteParameter(int id)
        {
            ViewBag.Parameters = _dbContext.Parameters.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DeleteParameter(int id, Parameter parameter)
        {
            _dbContext.Parameters.Remove(parameter);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));


        }
        //Видалення пристрою
        public ActionResult DeleteDevice(int id)
        {
            ViewBag.Devices = _dbContext.Devices.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult DeleteDevice(int id, Device device)
        {
            try
            {
                _dbContext.Devices.Remove(device);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(device);
            }
        }
        public ActionResult EditDevice(int id)
        {
            ViewBag.Parameters = new SelectList(_dbContext.Parameters.ToList(), "Id", "Name");
            var device = _dbContext.Devices.Find(id);
            return View(device);
        }

        [HttpPost]
        public ActionResult EditDevice(Device device)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(device).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Parameters = new SelectList(_dbContext.Parameters.ToList(), "Id", "Name");
                return View(device);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                ViewBag.Parameters = new SelectList(_dbContext.Parameters.ToList(), "Id", "Name");
                return View(device);
            }
        }

        public IActionResult EditParameter(int id)
        {
            // Retrieve the parameter from the database based on the provided ID
            Parameter parameter = _dbContext.Parameters.Find(id);

            if (parameter == null)
            {
                // If the parameter is not found, you might want to handle this scenario (e.g., show an error message)
                return NotFound();
            }

            return View(parameter);
        }

        // New action for handling the form submission for editing a parameter
        [HttpPost]
        public IActionResult EditParameter(Parameter parameter)
        {
            if (ModelState.IsValid)
            {
                // Update the parameter in the database
                _dbContext.Parameters.Update(parameter);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // If the model is invalid, return the user to the edit parameter page with error messages
            return View(parameter);
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
