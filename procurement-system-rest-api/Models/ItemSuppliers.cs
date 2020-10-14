namespace procurement_system_rest_api.Models
{
    public class ItemSuppliers
    {
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public string SupplierCode { get; set; }
        public Supplier Supplier { get; set; }
    }
}
