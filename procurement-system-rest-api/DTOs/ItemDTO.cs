namespace procurement_system_rest_api.DTOs
{
    public class ItemDTO
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string Description { get; set; }
        public string UnitOfMeasuring { get; set; }

        public string SupplierCode { get; set; }
    }
}
