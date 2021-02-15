using CarInventorySystem_Project.Data;
using CarInventorySystem_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CarInventorySystem_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarInventorySystem_ProjectContext _context;

        public HomeController(ILogger<HomeController> logger, CarInventorySystem_ProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            this.SetData();
        }

        public IActionResult Index()
        {
            return View();
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

        private void SetData()
        {
            ViewData["TotalBrands"] = _context.Brand.Count();
            ViewData["TotalModels"] = _context.CarModel.Count();
            ViewData["TotalCars"] = _context.Car.Count();
        }
    }
}
