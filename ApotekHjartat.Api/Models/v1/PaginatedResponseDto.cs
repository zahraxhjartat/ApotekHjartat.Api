using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApotekHjartat.Api.Models.v1
{
    public class PaginatedResponseDto<T>
    {
        [Required]
        public int TotalCount { get; set; }
        public int? Next { get; set; }
        public List<T> Page { get; set; }
    }
}
