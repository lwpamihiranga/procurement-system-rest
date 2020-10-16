using System;
using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class Site
    {
        [Key]
        public string SiteCode { get; set; }
        public string SiteName { get; set; }
        public SiteManager SiteManager { get; set; } 
        public string SiteAddress { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SiteOfficeNo { get; set; }
    }
}
