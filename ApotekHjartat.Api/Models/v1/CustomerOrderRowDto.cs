
namespace ApotekHjartat.Api.Models.v1
{
    public class CustomerOrderRowDto
    {
        public int? OrderRowId { get; set; }
        public int OrderedAmount { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal PriceExclVat { get; set; }
        public decimal Vat { get; set; }
        public CustomerOrderRowTypeDto OrderRowType { get; set; }
    }
}
