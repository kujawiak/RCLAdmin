using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Data;
using RCLAdmin.Core.Models;

namespace RCLAdmin.Core.Controllers
{
    public class PrintersController : Controller
    {
        private readonly RCLAdminContext _context;

        public PrintersController(RCLAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var printers = _context
                .Printers
                .Where(a => a.Status == true)
                .Include(a => a.PrinterType)
                .ToListAsync();
            return View(await printers);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var printer = await _context.Printers
                .Include(a => a.PrinterType)
                .SingleOrDefaultAsync(m => m.PrinterId == id);
            if (printer == null)
            {
                return NotFound();
            }
            populatePrinterAccessoryDropDown(printer.PrinterType);
            return View(printer);
        }

        public IActionResult Create()
        {
            populatePrinterTypeDropDown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrinterId,IP,Localisation,SerialNumber,Photo,Info,Person,Status,PrinterType")] Printer printer)
        {
            printer.PrinterType = _context
                .PrinterTypes
                .SingleOrDefault(a => a.PrinterTypeId == printer.PrinterType.PrinterTypeId);

            if (ModelState.IsValid)
            {
                _context.Add(printer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            populatePrinterTypeDropDown();
            return View(printer);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var printer = await _context
                .Printers
                .Include(a => a.PrinterType)
                .SingleOrDefaultAsync(m => m.PrinterId == id);
            if (printer == null)
            {
                return NotFound();
            }

            populatePrinterTypeDropDown();
            return View(printer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrinterId,IP,Localisation,SerialNumber,Photo,Info,Person,Status,PrinterType")] Printer printer)
        {
            if (id != printer.PrinterId)
            {
                return NotFound();
            }

            printer.PrinterType = _context
                .PrinterTypes
                .SingleOrDefault(a => a.PrinterTypeId == printer.PrinterType.PrinterTypeId);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(printer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrinterExists(printer.PrinterId))
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
            return View(printer);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var printer = await _context.Printers
                .Include(a => a.PrinterType)
                .SingleOrDefaultAsync(m => m.PrinterId == id);
            if (printer == null)
            {
                return NotFound();
            }

            return View(printer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var printer = await _context.Printers.SingleOrDefaultAsync(m => m.PrinterId == id);
            //_context.Printers.Remove(printer);
            printer.Status = false;
            _context.Update(printer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrinterExists(int id)
        {
            return _context.Printers.Any(e => e.PrinterId == id);
        }

        private void populatePrinterTypeDropDown()
        {
            var list = new List<SelectListItem>();
            foreach (var item in _context.PrinterTypes.OrderBy(a => a.PrinterManufacturer))
            {
                list.Add(new SelectListItem()
                {
                    Text = String.Format("{0} {1} (ID:{2})", item.PrinterManufacturer, item.Type, item.PrinterTypeId),
                    Value = item.PrinterTypeId.ToString()
                });
            }
            ViewData["PrinterTypes"] = list;
        }

        private void populatePrinterAccessoryDropDown(PrinterType printerType)
        {
            var list = new List<SelectListItem>();
            var accessories = _context.PrinterAccessories
                .Where(a => a.PrinterTypes.Any(b => b.PrinterTypeId == printerType.PrinterTypeId));
            foreach (var item in accessories)
            {
                list.Add(new SelectListItem()
                {
                    Text = string.Format("{0} {1} (ID:{2})", item.Name, item.PartNumber, item.PrinterAccessoryId),
                    Value = item.PrinterAccessoryId.ToString()
                });
            }
            ViewData["PrinterAccessories"] = list;
        }
    }
}
