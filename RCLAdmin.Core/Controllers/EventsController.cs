using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Data;
using RCLAdmin.Core.Models;

namespace RCLAdmin.Core.Controllers
{
    public class EventsController : Controller
    {
        private readonly RCLAdminContext _context;

        public EventsController(RCLAdminContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = _context.PrinterEvents
                .OrderByDescending(a => a.Date)
                .ToListAsync();
            return View(await events);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PrinterEvent printerEvent)
        {
            if (printerEvent.PrinterAccessory == null)
            {
                return null;
            }
            printerEvent.Date = DateTime.Now;
            Printer printer = _context.Printers.Find(printerEvent.Printer.PrinterId);
            PrinterAccessory accessory = _context.PrinterAccessories.Find(printerEvent.PrinterAccessory.PrinterAccessoryId);
            printerEvent.Printer = printer;
            printerEvent.PrinterAccessory = accessory;
            _context.PrinterEvents.Add(printerEvent);

            var accessoryToModify = _context.Entry(accessory);
            accessoryToModify.Entity.Availability = accessoryToModify.Entity.Availability - 1;
            accessoryToModify.State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return ViewComponent("PrinterEvents", new { printerId = printerEvent.Printer.PrinterId });
        }
    }
}