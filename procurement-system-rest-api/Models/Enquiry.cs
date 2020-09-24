using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Enquiry
    {
        public int EnquiryId { get; set; }
        public string OrderReference { get; set; }
        public string SiteManagerId { get; set; }
        public string OrderType { get; set; }
        public string Description { get; set; }
        public string EnquiryStatus { get; set; }
    }
}
