using GroupProjectBackend.Models.DB;
using System.Collections.Generic;

namespace GroupProjectBackend.Models.Dto
{
    public class SearchingDto
    {
        public string Label { get; set; }
        public string FullAddress { get; set; }
        public int CategoryId { get; set; }
        public int AverageRating { get; set; }
    }
    
}
