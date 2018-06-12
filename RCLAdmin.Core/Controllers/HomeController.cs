using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RCLAdmin.Core.Data;
using RCLAdmin.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace RCLAdmin.Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly RCLAdminContext _context;

        public HomeController(RCLAdminContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var printers = _context.Printers.Include(a => a.PrinterType).Include(a => a.Events).ToList();
            var events = _context.PrinterEvents.Include(a => a.PrinterAccessory).ToList();
            var x = _context.PrinterAccessories.Include(a => a.PrinterTypes).ToList();

            ViewData["Data"] = printers;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
