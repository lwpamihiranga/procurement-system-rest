using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace procurement_system_rest_api.Models
{
    public class Item
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasuring { get; set; }
        // add suppliers list below
    }
}
