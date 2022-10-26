using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApotekHjartat.DbAccess.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        [StringLength(100)]
        public string ExternalProductId { get; set; }
        [StringLength(100)]

        public string Name { get; set; }
        public bool InStock { get; set; }
        public Decimal PriceExclVat { get; set; }
        public Decimal Vat { get; set; }
        [StringLength(20)]
        public string VaraArticleType { get; set; }
        public string Ean { get; set; }
    }
}