using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApotekHjartat.Order.DbAccess.Models
{
    public class CustomerOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerOrderId { get; set; }
        [StringLength(100)]
        public string OrderNumber { get; set; }
        public bool IsRxOrder { get; set; }
    }
}
