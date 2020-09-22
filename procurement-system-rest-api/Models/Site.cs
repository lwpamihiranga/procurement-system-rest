using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Site
    {
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public string SiteManagerId { get; set; }
        public string SiteAddress { get; set; }
        public string Description { get; set; }
        public string ConstructionPeriod { get; set; }
        public string SiteOfficeNo { get; set; }
    }
}
