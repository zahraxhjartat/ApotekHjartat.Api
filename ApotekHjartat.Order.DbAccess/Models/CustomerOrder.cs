using System.Collections.Generic;
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
        public string CustomerFirstName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerAddress { get; set; }
        public string OrderStatus { get; set; }
        [InverseProperty("CustomerOrder")]
        public virtual ICollection<CustomerOrderRow> CustomerOrderRow { get; set; }
    }
}
