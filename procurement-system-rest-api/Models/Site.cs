using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Site
    {
        [Key]
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public SiteManager SiteManager { get; set; } // string SiteManagerId -> SiteManager SiteManager
        public string SiteAddress { get; set; }
        public string Description { get; set; }
        public string ConstructionPeriod { get; set; }
        public string SiteOfficeNo { get; set; }
    }
}
