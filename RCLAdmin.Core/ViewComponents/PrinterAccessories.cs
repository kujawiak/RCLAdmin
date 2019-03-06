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
    public class PrinterAccessories : ViewComponent
    {
        private readonly RCLAdminContext _context;

        public PrinterAccessories(RCLAdminContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int printerTypeId)
        {
            var items = await GetItemsAsync(printerTypeId);
            return View(items);
        }

        private Task<List<PrinterAccessory>> GetItemsAsync(int printerTypeId)
        {
            //return
            var result = _context.PrinterAccessories
            .Where(a => a.PrinterAccessoryType.Where(b => b.PrinterType.PrinterTypeId == printerTypeId).Any());
                
            return result.ToListAsync();
        }
    }
}
