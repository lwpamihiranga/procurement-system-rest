namespace procurement_system_rest_api.DTOs
{
    public class EnquiryDTO
    {
        public int EnquiryId { get; set; }
        public int OrderReference { get; set; }
        public string SiteManagerId { get; set; }  
        public string OrderType { get; set; } 
        public string Description { get; set; }
        public string EnquiryStatus { get; set; }
    }
}
