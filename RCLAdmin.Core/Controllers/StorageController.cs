using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Data;
using RCLAdmin.Core.Models;

namespace RCLAdmin.Core.Controllers
{
    public class StorageController : Controller
    {
        private readonly RCLAdminContext _context;

        public StorageController(RCLAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var accessories = _context
                .PrinterAccessories
                .Include(a => a.PrinterAccessoryType)
                .ThenInclude(a => a.PrinterType)
                .Where(a => !a.Name.ToLower().Contains("drum"))
                .ToListAsync();

            var printerTypes = await _context.PrinterTypes.ToListAsync();
            var viewData = await accessories;

            ViewBag.PrinterTypes = printerTypes;
            return View(viewData);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accessory = await _context
                .PrinterAccessories
                .SingleOrDefaultAsync(m => m.PrinterAccessoryId == id);
            if (accessory == null)
            {
                return NotFound();
            }

            return View(accessory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PrinterAccessory accessory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accessory);
                    //TODO: Notify registry about this update
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccessoryExists(accessory.PrinterAccessoryId))
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
            return RedirectToAction(nameof(Index));
        }

        private bool AccessoryExists(int id)
        {
            return _context.PrinterAccessories.Any(e => e.PrinterAccessoryId == id);
        }
    }
}