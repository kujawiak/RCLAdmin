using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Data;
using RCLAdmin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.ViewComponents
{
    public class PrinterEvents : ViewComponent
    {
        private readonly RCLAdminContext _context;

        public PrinterEvents(RCLAdminContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int printerId)
        {
            var items = await GetItemsAsync(printerId);
            return View(items);
        }

        private Task<List<PrinterEvent>> GetItemsAsync(int printerId)
        {
            return _context.PrinterEvents
                .Include(a => a.PrinterAccessory)
                .Where(a => a.Printer.PrinterId == printerId)
                .OrderByDescending(a => a.Date)
                .ToListAsync();
        }
    }
}
