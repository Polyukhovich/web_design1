using Electronic.Data;
using Electronic.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electronic.Controllers
{
    public class ParameterController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ParameterController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: ParameterController
        public ActionResult Index()
        {
            var parameters = _dbContext.Parameters.ToList();
            return View(parameters);
        }

        // GET: ParameterController/Details/5
        public ActionResult Details(int id)
        {
            var parameter = _dbContext.Parameters.Find(id);
            return View(parameter);
        }

        // GET: ParameterController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParameterController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Parameter parameter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Parameters.Add(parameter);
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(parameter);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(parameter);
            }
        }

        // GET: ParameterController/Edit/5
        public ActionResult Edit(int id)
        {
            var parameter = _dbContext.Parameters.Find(id);
            return View(parameter);
        }

        // POST: ParameterController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Parameter parameter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Entry(parameter).State = EntityState.Modified;
                    _dbContext.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
                return View(parameter);
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(parameter);
            }
        }

        // GET: ParameterController/Delete/5
        public ActionResult Delete(int id)
        {
            var parameter = _dbContext.Parameters.Find(id);
            return View(parameter);
        }

        // POST: ParameterController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Parameter parameter)
        {
            try
            {
                _dbContext.Parameters.Remove(parameter);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Обробка помилок
                return View(parameter);
            }
        }
    }
}
