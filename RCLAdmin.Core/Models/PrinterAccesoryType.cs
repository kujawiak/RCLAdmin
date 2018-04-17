namespace RCLAdmin.Core.Models
{
    public class PrinterAccessoryType
    {
        public int PrinterTypeId { get; set; }
        public PrinterType PrinterType { get; set; }
        public int PrinterAccessoryId { get; set; }
        public PrinterAccessory PrinterAccessory { get; set; }
    }
}