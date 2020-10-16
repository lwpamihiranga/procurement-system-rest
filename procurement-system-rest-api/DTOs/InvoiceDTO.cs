using System;

namespace procurement_system_rest_api.DTOs
{
    public class InvoiceDTO
    {
        public string InvoiceId { get; set; }
        public int GoodsReceipt { get; set; } 
        public string Supplier { get; set; }   
        public string AccountingStaffId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceStatus { get; set; }
        public string Description { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double TaxAmount { get; set; }
        public double CreditNote { get; set; }
        public double AdvancePayment { get; set; }
    }
}
