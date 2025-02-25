﻿using System;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Invoice
    {
        [Key]
        public string InvoiceId { get; set; }
        public GoodsReceipt GoodsReceipt { get; set; }
        public Supplier Supplier { get; set; }
        public AccountingStaff HandledAccountant { get; set; }
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
