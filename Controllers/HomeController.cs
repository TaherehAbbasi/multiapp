using MultiApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DockerCrud.Controllers
{
    public class HomeController : Controller
    {
        private MovieContext context;
        public HomeController(MovieContext mc)
        {
            context = mc;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie m)
        {
            if (ModelState.IsValid)
            {
                context.Add(m);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
                return View();
        }

        public IActionResult Index()
        {
            var m = context.Movie.ToList();
            return View(m);
        }

        public IActionResult Update(int id)
        {
            var pc = context.Movie.Where(a => a.Id == id).FirstOrDefault();
            return View(pc);
        }

        [HttpPost]
        public async Task<IActionResult> Update(Movie m)
        {
            if (ModelState.IsValid)
            {
                context.Update(m);
                await context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            else
                return View(m);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var pc = context.Movie.Where(a => a.Id == id).FirstOrDefault();
            context.Remove(pc);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}