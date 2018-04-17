using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.Models
{
    public class PrinterEvent
    {
        public int PrinterEventId { get; set; }

        [Required]
        public Printer Printer { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public PrinterEventType EventType { get; set; }

        [Required]
        public int Counter { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public PrinterAccessory PrinterAccessory { get; set; }

        public string Comment { get; set; }
    }
}
