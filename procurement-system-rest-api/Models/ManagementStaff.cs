using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class ManagementStaff : CompanyStaff
    {
        public List<PurchaseOrder> OrdersApproved { get; set; }
    }
}
