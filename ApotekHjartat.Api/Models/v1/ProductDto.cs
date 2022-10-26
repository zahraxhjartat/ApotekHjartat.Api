using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApotekHjartat.Api.Models.v1
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool InStock { get; set; }
        public Decimal PriceExclVat { get; set; }
        public Decimal Vat { get; set; }
        public string VaraArticleType { get; set; }
        public bool IsPrescription { get; set; }
    }
}
