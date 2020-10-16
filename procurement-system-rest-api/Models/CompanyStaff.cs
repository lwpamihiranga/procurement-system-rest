using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class CompanyStaff
    {
        [Key]
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkNo { get; set; }
        public string HomeNo { get; set; }
        public string MobileNo { get; set; }
    }
}
