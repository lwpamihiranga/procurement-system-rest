using System.Collections.Generic;

namespace procurement_system_rest_api.Models
{
    public class AccountingStaff : CompanyStaff
    {
        public List<Invoice> InvoicesHandled { get; set; }
    }
}
