using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bleemsMVC.Data;
using bleemsMVC.Models;
using NToastNotify;
using System.Globalization;


namespace bleemsMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly bleemsContext _context;
        private readonly IToastNotification _toastNotification;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductsController(bleemsContext context, IToastNotification toastNotification, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _toastNotification = toastNotification;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Products
        //public async Task<IActionResult> Index()
        //{

        //    ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");

        //    var bleemsContext = _context.Products.Include(p => p.Category);
        //    return View(await bleemsContext.ToListAsync());
        //}



        public async Task<IActionResult> Index(int? selectedYear, int? selectedMonth, int? selectedDay)
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");

            IQueryable<Product> bleemsContext = _context.Products.Include(p => p.Category);

            // Filter by selectedYear
            if (selectedYear != null)
            {
                bleemsContext = bleemsContext.Where(p => p.DateAdded.Year == selectedYear);
            }

            // Filter by selectedMonth
            if (selectedMonth != null)
            {
                bleemsContext = bleemsContext.Where(p => p.DateAdded.Month == selectedMonth);
            }

            // Filter by selectedDay
            if (selectedDay != null)
            {
                bleemsContext = bleemsContext.Where(p => p.DateAdded.Day == selectedDay);
            }

            // Pass the selected values to the view
            ViewData["SelectedYear"] = selectedYear;
            ViewData["SelectedMonth"] = selectedMonth;
            ViewData["SelectedDay"] = selectedDay;

            return View(await bleemsContext.ToListAsync());
        }





        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598. [Bind("Id,Name,Description,Price,Photo,CategoryId,DateAdded")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Product product)
        {
            if (ModelState.IsValid)
            {

                if (product.contractdoc != null)
                {
                    string folder = "ProductData\\uploads\\";
                    folder += product.contractdoc.FileName;
                    string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                    product.contractdoc.CopyToAsync(new FileStream(serverFolder, FileMode.Create));


                    var imagename = product.contractdoc.FileName;

                    var newProduct = new Product()
                    {
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Photo = Path.Combine(_webHostEnvironment.WebRootPath, "ProductData\\uploads\\", imagename),
                        CategoryId = product.CategoryId,
                        DateAdded = DateTime.Now
                    };



                    _context.Products.Add(newProduct);


                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Added Successfully");


                }


               
               



                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", product.CategoryId);
            return PartialView(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Price,Photo,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    _context.Products.Attach(product);
                    _context.Entry(product).Property(x => x.Name).IsModified = true;
                    _context.Entry(product).Property(x => x.Description).IsModified = true;
                    _context.Entry(product).Property(x => x.Price).IsModified = true;
                    _context.Entry(product).Property(x => x.Photo).IsModified = true;
                    _context.Entry(product).Property(x => x.CategoryId).IsModified = true;

                    await _context.SaveChangesAsync();
                    _toastNotification.AddSuccessToastMessage("Updated Successfully");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Id", product.CategoryId);
            return PartialView(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return PartialView(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'bleemsContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();

            _toastNotification.AddSuccessToastMessage("Deleted Successfully");
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
