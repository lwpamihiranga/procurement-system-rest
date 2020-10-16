using System;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Payment
    {
        [Key]
        public string PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public Invoice Invoice { get; set; } 
        public DateTime DueDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string PaymentStatus { get; set; }
        public double PaidAmount { get; set; }
        public string Description { get; set; }
    }
}
