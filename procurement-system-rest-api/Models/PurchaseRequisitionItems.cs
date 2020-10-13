namespace procurement_system_rest_api.Models
{
    public class PurchaseRequisitionItems
    {
        public int RequisitionNo { get; set; }
        public PurchaseRequisition PurchaseRequisition { get; set; }
        public string ItemId { get; set; }
        public Item Item { get; set; }
    }
}
