using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Payment
    {
        public string PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public string PurchaseOrderId { get; set; }
        public string InvoiceId { get; set; }
        public DateTime DueDate { get; set; }  // Date -> Datetime
        public DateTime PaidDate { get; set; }  // Date -> Datetime
        public string PaymentStatus { get; set; }
        public double PaidAmount { get; set; }
        public string Description { get; set; }
    }
}
