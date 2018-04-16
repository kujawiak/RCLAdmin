using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.Models
{
    public class PrinterAccessory
    {
        public PrinterAccessory()
        {
            this.PrinterTypes = new HashSet<PrinterType>();
        }
        public int PrinterAccessoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public string PartNumber { get; set; }
        public ICollection<PrinterType> PrinterTypes { get; set; }
        [Display(Name = "Dostępność")]
        public int Availability { get; set; }
        public override string ToString()
        {
            return string.Format("{0} P/N:{1}", Name, PartNumber);
        }
    }
}
