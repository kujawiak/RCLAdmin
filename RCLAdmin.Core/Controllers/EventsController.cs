using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCLAdmin.Core.Models;

namespace RCLAdmin.Core.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult Index() //int printerId
        {
            //var events = eventsRepository.GetEventsForPrinterId(printerId);
            //return PartialView("Partials/ListPrinterEvents", events);
            return Content("TEST");
        }

        [HttpPost]
        public ActionResult Create(PrinterEvent printerEvent) //async Task<IActionResult>
        {
            //ViewBag.PrinterEventTypeList = dropDownListHelper.PopulatePrinterEventTypeDropDownList();
            //eventsRepository.InsertEvent(printerEvent);
            //printersRepository.UpdatePrinterAccessoryAvailability(printerEvent.PrinterAccessory, -1);
            return RedirectToAction("Index", "Events"); //new { printerId = printerEvent.Printer.PrinterId }
        }
    }
}