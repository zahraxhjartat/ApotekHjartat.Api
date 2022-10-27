using ApotekHjartat.DbAccess.Enums;
using System;

namespace ApotekHjartat.DbAccess.Models
{
   public class CustomerOrderFilter
    {
        public CustomerOrderStatus? CustomerOrderStatus { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? OrderNumber { get; set; }

        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
