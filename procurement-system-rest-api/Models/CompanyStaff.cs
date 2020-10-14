using System.ComponentModel.DataAnnotations;

namespace procurement_system_rest_api.Models
{
    public class CompanyStaff
    {
        [Key]
        public string StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WorkNo { get; set; } // int -> string
        public string HomeNo { get; set; } // int -> string
        public string MobileNo { get; set; } // int -> string
    }
}
