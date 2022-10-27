using ApotekHjartat.Api.Enums;
using ApotekHjartat.Api.Models.v1;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApotekHjartat.Api.Models
{
    public class CustomerOrderDto
    {
        public int CustomerOrderId { get; set; }
        public string OrderNumber { get; set; }
        public List<CustomerOrderRowDto> CustomerOrderRows { get; set; }
        public CustomerOrderStatusDto OrderStatus { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime Created { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerSurname { get; set; }
        public string CustomerEmailAddress { get; set; }
        public string CustomerAddress { get; set; }

    }
}
