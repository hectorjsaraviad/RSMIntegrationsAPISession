namespace RSMEnterpriseIntegrationsAPI.Application.DTOs
{
    public class UpdateSalesOrderHeaderDto
    {
        public int SalesOrderID { get; set; }
        public decimal? SubTotal { get; set; }
        public decimal? TaxAmt { get; set; }
        public decimal? Freight { get; set; }

    }

}
