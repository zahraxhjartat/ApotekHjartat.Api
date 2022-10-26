
namespace ApotekHjartat.Api.Models.v1
{
    public class BasketRowDto
    {
        public int OrderedAmount { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal PriceExclVat { get; set; }
        public decimal Vat { get; set; }
        public CustomerOrderRowTypeDto BasketRowType { get; set; }
    }
}
