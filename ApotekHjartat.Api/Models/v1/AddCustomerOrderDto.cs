using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApotekHjartat.Api.Models.v1
{
    public class AddCustomerOrderDto
    {
        [Required]
        public string CustomerEmailAddress { get; set; }
        [Required]
        public string CustomerFirstName { get; set; }
        [Required]
        public string CustomerSurname { get; set; }
        [Required]
        public string CustomerAddress { get; set; }
        [Required]
        public List<BasketRowDto> CustomerOrderRows { get; set; }
    }
}
