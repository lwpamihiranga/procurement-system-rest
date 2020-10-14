using Microsoft.EntityFrameworkCore;
using procurement_system_rest_api.Models;

namespace procurement_system_rest_api
{
    public class ProcurementDbContext : DbContext
    {
        public ProcurementDbContext(DbContextOptions<ProcurementDbContext> options) : base(options) { }

        public DbSet<SiteManager> SiteManagers {get; set;}
        public DbSet<Site> Sites { get; set; }
        public DbSet<ManagementStaff> ManagementStaff { get; set; }
        public DbSet<PurchaseRequisition> PurchaseRequisitions { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ItemSuppliers>()
                .HasKey(si => new { si.ItemId, si.SupplierCode });
            modelBuilder.Entity<ItemSuppliers>()
                .HasOne(si => si.Item)
                .WithMany(i => i.ItemSuppliers)
                .HasForeignKey(si => si.ItemId);
            modelBuilder.Entity<ItemSuppliers>()
                .HasOne(si => si.Supplier)
                .WithMany(s => s.ItemSuppliers)
                .HasForeignKey(si => si.SupplierCode);

            modelBuilder.Entity<PurchaseRequisitionItems>()
                .HasKey(pri => new { pri.RequisitionNo, pri.ItemId });
            modelBuilder.Entity<PurchaseRequisitionItems>()
                .HasOne(pri => pri.PurchaseRequisition)
                .WithMany(pr => pr.PurchaseRequisitionItems)
                .HasForeignKey(pri => pri.RequisitionNo);
            modelBuilder.Entity<PurchaseRequisitionItems>()
                .HasOne(pri => pri.Item)
                .WithMany(i => i.PurchaseRequisitionItems)
                .HasForeignKey(pri => pri.ItemId);

            modelBuilder.Entity<PurchaseOrderItems>()
                .HasKey(poi => new { poi.OrderReference, poi.ItemId });
            modelBuilder.Entity<PurchaseOrderItems>()
                .HasOne(poi => poi.PurchaseOrder)
                .WithMany(po => po.PurchaseOrderItems)
                .HasForeignKey(poi => poi.OrderReference);
            modelBuilder.Entity<PurchaseOrderItems>()
                .HasOne(poi => poi.Item)
                .WithMany(i => i.PurchaseOrderItems)
                .HasForeignKey(poi => poi.ItemId);
        }
    }
}
