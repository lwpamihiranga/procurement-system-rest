namespace procurement_system_rest_api.Models
{
    public class PurchaseOrderItems
    {
        public int OrderReference { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public int ItemCount { get; set; }
    }
}
