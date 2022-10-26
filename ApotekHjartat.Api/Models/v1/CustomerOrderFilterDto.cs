
using ApotekHjartat.Api.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ApotekHjartat.Api.Models.v1
{
    public class CustomerOrderFilterDto
    {
        public CustomerOrderStatusDto? CustomerOrderStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? OrderNumber { get; set; }

        [Required]
        public int Skip { get; set; }
        [Required]
        [Range(1, 500)]
        public int Take { get; set; }
    }
}
