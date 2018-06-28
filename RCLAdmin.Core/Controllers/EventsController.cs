using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RCLAdmin.Core.Data;
using RCLAdmin.Core.Models;
using TemplateEngine.Docx;

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
                .Include(a => a.Printer)
                .ThenInclude(a => a.PrinterType)
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
            printerEvent.Printer = _context.Printers.Find(printerEvent.Printer.PrinterId);

            if (printerEvent.EventType == PrinterEventType.ZgloszenieAwarii)
            {
                printerEvent.PrinterAccessory = _context.PrinterAccessories.Find(1);
            }
            else
            {
                PrinterAccessory accessory = _context.PrinterAccessories.Find(printerEvent.PrinterAccessory.PrinterAccessoryId);
                printerEvent.PrinterAccessory = accessory;

                var accessoryToModify = _context.Entry(accessory);
                accessoryToModify.Entity.Availability = accessoryToModify.Entity.Availability - 1;
                accessoryToModify.State = EntityState.Modified;
            }

            _context.PrinterEvents.Add(printerEvent);
            await _context.SaveChangesAsync();

            return ViewComponent("PrinterEvents", new { printerId = printerEvent.Printer.PrinterId });
        }

        public ActionResult CrashNotificationForm(int printerId, int printerEventId)
        {
            var printer = _context.Printers.Include(a => a.PrinterType).SingleOrDefault(a => a.PrinterId == printerId);
            if (printer == null)
                return NotFound();
            var eevent = _context.PrinterEvents.Find(printerEventId);
            if (eevent == null)
                return NotFound();
            var filename = String.Format("Zgłoszenie awarii RCL - {0} ({1}) {2}.docx",
                printer.PrinterType.ToString(),
                printer.SerialNumber,
                eevent.Date.ToShortDateString());
            var outputFile = @"wwwroot/zgloszenia/" + filename;
            try
            {
                System.IO.File.Copy(@"wwwroot/template_0.docx", outputFile, true);
                if (eevent.Comment == null) eevent.Comment = String.Empty;
                if (printer.SerialNumber == null) printer.SerialNumber = "brak";

                var valuesToFill = new Content(
                    new FieldContent("PRINTER", printer.PrinterType.ToString()),
                    new FieldContent("SERIAL_NUMBER", printer.SerialNumber),
                    new FieldContent("COMMENT", eevent.Comment),
                    new FieldContent("DATE", eevent.Date.ToString("yyyy-MM-dd HH:mm"))
                    );

                using (var outputDocument = new TemplateProcessor(outputFile)
                    .SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(valuesToFill);
                    outputDocument.SaveChanges();
                }

                byte[] fileBytes = System.IO.File.ReadAllBytes(outputFile);
                return File(fileBytes, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", filename);
            }
            catch (Exception ex)
            {
                return Content(String.Format("{0}", ex.Message));
            }           
        }
    }
}