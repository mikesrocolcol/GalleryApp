using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Inventory.Data;
using Inventory.Models;


namespace Inventory.Controllers
{
    public class ProductEntitiesController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly InventoryContext _context;
   

        public ProductEntitiesController(InventoryContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _hostEnvironment = webHost;
        }

        // GET: ProductEntities
        public async Task<IActionResult> Index()
        {
              return _context.Inventories != null ? 
                          View(await _context.Inventories.ToListAsync()) :
                          Problem("Entity set 'InventoryContext.Inventories'  is null.");
        }



       

        // GET: ProductEntities/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductEntities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image,FormFile")] ProductEntities productEntity)
        {
            if (ModelState.IsValid)
            {
                /**//*Save images to wwwroot*//**/
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(productEntity.FormFile.FileName);
                string extension = Path.GetExtension(productEntity.FormFile.FileName);
                productEntity.Image = fileName = fileName + extension;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await productEntity.FormFile.CopyToAsync(fileStream);
                }




                _context.Add(productEntity);
                await _context.SaveChangesAsync();
                TempData["success"] = "Created successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(productEntity);
        }

        

        // GET: ProductEntities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Inventories.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }
            return View(productEntity);
        }

        // POST: ProductEntities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Image")] ProductEntities productEntity)
        {
            if (id != productEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productEntity);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductEntityExists(productEntity.Id))
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
            return View(productEntity);
        }

        // GET: ProductEntities/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventories == null)
            {
                return NotFound();
            }

            var productEntity = await _context.Inventories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productEntity == null)
            {
                return NotFound();
            }

            return View(productEntity);
        }

        // POST: ProductEntities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventories == null)
            {
                return Problem("Entity set 'InventoryContext.Inventories'  is null.");
            }
            var productEntity = await _context.Inventories.FindAsync(id);
            if (productEntity != null)
            {
                _context.Inventories.Remove(productEntity);
            }
            
            await _context.SaveChangesAsync();
            TempData["success"] = "Deleted successfully";
            return RedirectToAction(nameof(Index));
        }

        private bool ProductEntityExists(int id)
        {
          return (_context.Inventories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        


    }
}
