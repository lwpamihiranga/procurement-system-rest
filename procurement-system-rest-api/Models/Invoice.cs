using System;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Invoice
    {
        [Key]
        public string InvoiceId { get; set; }
        public GoodsReceipt GoodsReceipt { get; set; } //string GoodsReceiptId -> GoodsReceipt GoodsReceipt
        public Supplier Supplier { get; set; }    //string SupplierName -> Supplier Supplier
       // public string SupplierCompany { get; set; }   //include in supplier model
        
        public AccountingStaff HandledAccountant { get; set; }
        //public string CreatedById { get; set; }
        //public string CreatedByName { get; set; }
        public DateTime InvoiceDate { get; set; } // Date -> Datetime
        public string InvoiceStatus { get; set; }
        public string Description { get; set; }
        //public List<Item> Items { get; set; }
        public double NetAmount { get; set; }
        public double GrossAmount { get; set; }
        public double TaxAmount { get; set; }
        public double CreditNote { get; set; }
        public double AdvancePayment { get; set; }
    }
}
