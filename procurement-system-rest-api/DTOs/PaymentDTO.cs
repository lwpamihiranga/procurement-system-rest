using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.DTOs
{
    public class PaymentDTO
    {
        public string PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public string Invoice { get; set; }  
        public DateTime DueDate { get; set; }  
        public DateTime PaidDate { get; set; }  
        public string PaymentStatus { get; set; }
        public double PaidAmount { get; set; }
        public string Description { get; set; }
    }
}
