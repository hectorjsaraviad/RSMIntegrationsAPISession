namespace RSMEnterpriseIntegrationsAPI.Domain.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? ProductNumber { get; set; }
        public string? Color { get; set; }
        public decimal StandardCost { get; set; }
        public decimal ListPrice { get; set; }
        public string? Size { get; set; }
        public decimal? Weight { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public bool MakeFlag { get; set; }
        public short ReorderPoint { get; set; }
        public short SafetyStockLevel { get; set; }
        public int DaysToManufacture { get; set; }
        public DateTime SellStartDate { get; set; }
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }

}
