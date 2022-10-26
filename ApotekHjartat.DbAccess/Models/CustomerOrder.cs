using ApotekHjartat.DbAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApotekHjartat.DbAccess.Models
{
    public class CustomerOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerOrderId { get; set; }
        [StringLength(100)]
        public string OrderNumber { get; set; }
        public DateTime Created { get; set; }
        public string? TrackingNumber { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerAddress { get; set; }
        public CustomerOrderStatus OrderStatus { get; set; }
        [InverseProperty("CustomerOrder")]
        public virtual ICollection<CustomerOrderRow> CustomerOrderRows { get; set; }
    }
}
