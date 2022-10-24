using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApotekHjartat.Order.DbAccess.Models
{
    public class CustomerOrderRow
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderRowId { get; set; }

        public int OrderedAmount { get; set; }

        public int ProductId { get; set; }
        [StringLength(100)]
        public string ProductName { get; set; }
        public decimal PriceExclVat { get; set; }
        public decimal Vat { get; set; }
        public bool IsRx { get; set; }
        public int CustomerOrderId { get; set; }
        [ForeignKey(nameof(CustomerOrderId))]
        [InverseProperty("CustomerOrderRow")]
        public virtual CustomerOrder CustomerOrder { get; set; }
    }
}
