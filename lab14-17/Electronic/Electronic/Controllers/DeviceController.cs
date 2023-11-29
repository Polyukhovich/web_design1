using Electronic.Data;
using Electronic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electronic.Controllers
{
    public class DeviceController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public DeviceController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: DeviceController
        public ActionResult Index()
        {
            var devices = _dbContext.Devices.ToList();
            return View(devices);
        }

        // GET: DeviceController/Details/5
        public ActionResult Details(int id)
        {
            var device = _dbContext.Devices.Find(id);
            return View(device);
        }

        // GET: DeviceController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Device device)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Devices.Add(device);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(device);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(device);
            }
        }

        // GET: DeviceController/Edit/5
        public ActionResult Edit(int id)
        {
            var device = _dbContext.Devices.Find(id);
            return View(device);
        }

        // POST: DeviceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Device device)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(device).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(device);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(device);
            }
        }

        // GET: DeviceController/Delete/5
        public ActionResult Delete(int id)
        {
            var device = _dbContext.Devices.Find(id);
            return View(device);
        }

        // POST: DeviceController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Device device)
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
    }
}
