using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Data;

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
                .ToListAsync();
            var x = await accessories;
            return View(x);
        }
    }
}