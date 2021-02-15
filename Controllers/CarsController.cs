using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CarInventorySystem_Project.Data;
using CarInventorySystem_Project.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CarInventorySystem_Project.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarInventorySystem_ProjectContext _context;

        public CarsController(CarInventorySystem_ProjectContext context)
        {
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            this.SetData();
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            var carInventorySystem_ProjectContext = _context.Car.Include(c => c.Brand).Include(c => c.Model);
            return View(await carInventorySystem_ProjectContext.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", _context.Brand.FirstOrDefault());
            if (_context.Brand != null && _context.Brand.Count() > 0)
            {
                ViewData["ModelId"] = new SelectList(_context.CarModel.Where(x => x.BrandId == _context.Brand.FirstOrDefault().Id), "Id", "Model_Name");
            }
            else
            {
                ViewData["ModelId"] = new SelectList(_context.CarModel, "Id", "Model_Name");
            }
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturing_Date,Comments,Price,BrandId,ModelId")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel.Where(x => x.BrandId == car.BrandId), "Id", "Model_Name", car.ModelId);
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel.Where(x => x.BrandId == car.BrandId), "Id", "Model_Name", car.ModelId);
            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturing_Date,Comments,Price,BrandId,ModelId")] Car car)
        {
            if (id != car.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", car.BrandId);
            ViewData["ModelId"] = new SelectList(_context.CarModel.Where(x => x.BrandId == car.BrandId), "Id", "Model_Name", car.ModelId);
            return View(car);
        }

        // Json Call to get state  
        [HttpGet]
        public IActionResult GetModels(string id)
        {
            List<SelectListItem> models = new List<SelectListItem>();
            var modelList = _context.CarModel.Where(cm => cm.BrandId == Convert.ToInt32(id)).ToList();
            var modelData = modelList.Select(m => new SelectListItem()
            {
                Text = m.Model_Name,
                Value = m.Id.ToString(),
            });
            return Ok(modelData);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var car = await _context.Car
                .Include(c => c.Brand)
                .Include(c => c.Model)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }

        private void SetData()
        {
            ViewData["TotalBrands"] = _context.Brand.Count();
            ViewData["TotalModels"] = _context.CarModel.Count();
            ViewData["TotalCars"] = _context.Car.Count();
        }
    }
}
