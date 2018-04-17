using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.Models
{
    public class PrinterAccessory
    {
        public int PrinterAccessoryId { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "P/N")]
        public string PartNumber { get; set; }

        public List<PrinterAccessoryType> PrinterTypes { get; set; } 

        [Display(Name = "Dostępność")]
        public int Availability { get; set; }

        public override string ToString()
        {
            return string.Format("{0} P/N:{1}", Name, PartNumber);
        }
    }
}
