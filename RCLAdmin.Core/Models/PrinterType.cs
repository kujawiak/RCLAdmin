using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.Models
{
    public class PrinterType
    {
        public int PrinterTypeId { get; set; }

        [Display(Name = "Producent")]
        public PrinterManufacturer PrinterManufacturer { get; set; }

        [Display(Name = "Model")]
        public string Type { get; set; }

        public List<Printer> Printers { get; set; }

        public List<PrinterAccessoryType> PrinterAccessoryType { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", PrinterManufacturer, Type);
        }
    }
}
