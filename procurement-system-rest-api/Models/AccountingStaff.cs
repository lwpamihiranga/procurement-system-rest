using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class AccountingStaff : CompanyStaff
    {
        public List<Invoice> InvoicesHandled { get; set; }
    }
}
