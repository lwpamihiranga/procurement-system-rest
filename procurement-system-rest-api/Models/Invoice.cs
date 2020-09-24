using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Invoice
    {
        public string InvoiceId { get; set; }
        public string GoodsReceiptId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCompany { get; set; }
        public string CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public DateTime InvoiceDate { get; set; } // Date -> Datetime
        public string InvoiceStatus { get; set; }
        public string Description { get; set; }
        public List<Item> Items { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double TaxAmount { get; set; }
        public double CreditNote { get; set; }
        public double AdvancePayment { get; set; }
    }
}
