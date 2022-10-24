using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApotekHjartat.Order.Api.Models
{
    public class CustomerOrderDto
    {
        /// <summary>
        /// The Order Id
        /// </summary>
        [Required]
        public int CustomerOrderId { get; set; }
        /// <summary>
        /// The Customer Order Number
        /// </summary>
        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public bool ContainsRx { get; set; }
        [Required]
        public bool? ContainsChilled { get; set; }
        //public virtual ICollection<CustomerPackageDto> CustomerPackages { get; set; }
    }
}
