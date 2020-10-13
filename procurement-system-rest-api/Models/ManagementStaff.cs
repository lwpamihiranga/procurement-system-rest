using System.Collections.Generic;

namespace procurement_system_rest_api.Models
{
    public class ManagementStaff : CompanyStaff
    {
        public List<PurchaseOrder> OrdersApproved { get; set; }
    }
}
