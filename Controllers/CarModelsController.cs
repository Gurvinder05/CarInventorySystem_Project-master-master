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
    public class CarModelsController : Controller
    {
        private readonly CarInventorySystem_ProjectContext _context;

        public CarModelsController(CarInventorySystem_ProjectContext context)
        {
            _context = context;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            this.SetData();
        }

        // GET: CarModels
        public async Task<IActionResult> Index()
        {
            var carInventorySystem_ProjectContext = _context.CarModel.Include(c => c.Brand);
            return View(await carInventorySystem_ProjectContext.ToListAsync());
        }

        // GET: CarModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // GET: CarModels/Create
        public IActionResult Create()
        {
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name");
            return View();
        }

        // POST: CarModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Model_Name,BrandId,Start_Date,End_Date")] CarModel carModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", carModel.BrandId);
            return View(carModel);
        }

        // GET: CarModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel.FindAsync(id);
            if (carModel == null)
            {
                return NotFound();
            }
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", carModel.BrandId);
            return View(carModel);
        }

        // POST: CarModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Model_Name,BrandId,Start_Date,End_Date")] CarModel carModel)
        {
            if (id != carModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarModelExists(carModel.Id))
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
            ViewData["BrandId"] = new SelectList(_context.Brand, "Id", "Brand_Name", carModel.BrandId);
            return View(carModel);
        }

        // GET: CarModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carModel = await _context.CarModel
                .Include(c => c.Brand)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carModel == null)
            {
                return NotFound();
            }

            return View(carModel);
        }

        // POST: CarModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var activeItems = _context.Car.Where(x => x.ModelId == id);

            if (activeItems != null && activeItems.Count() > 0)
            {
                var carModel = await _context.CarModel
                    .Include(c => c.Brand)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (carModel == null)
                {
                    return NotFound();
                }

                ModelState.AddModelError("InUse", "Model is already being used by a car.");
                return View(carModel);
            }
            else
            {
                var carModel = await _context.CarModel.FindAsync(id);
                _context.CarModel.Remove(carModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        private bool CarModelExists(int id)
        {
            return _context.CarModel.Any(e => e.Id == id);
        }

        private void SetData()
        {
            ViewData["TotalBrands"] = _context.Brand.Count();
            ViewData["TotalModels"] = _context.CarModel.Count();
            ViewData["TotalCars"] = _context.Car.Count();
        }
    }
}
