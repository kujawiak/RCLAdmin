using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RCLAdmin.Core.Models
{
    public class Printer
    {
        public int PrinterId { get; set; }

        [Required]
        [RegularExpression(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b")]
        [Display(Name = "Adres IP")]
        public string IP { get; set; }

        [Required]
        [Display(Name = "Lokalizacja")]
        public string Localisation { get; set; }

        [Display(Name = "Numer seryjny")]
        public string SerialNumber { get; set; }

        [Required]
        [Display(Name = "Model")]
        public PrinterType PrinterType { get; set; }

        public byte[] Photo { get; set; }

        [Display(Name = "Info")]
        public string Info { get; set; }

        [Display(Name = "Użytkownik")]
        public string Person { get; set; }

        public List<PrinterEvent> Events { get; set; }

        public bool Status { get; internal set; } = true;

        public override string ToString()
        {
            return string.Format("{0} ({1})", SerialNumber, IP);
        }
    }
}
