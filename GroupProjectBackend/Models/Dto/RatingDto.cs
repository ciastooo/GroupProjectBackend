using GroupProjectBackend.Models.DB;
using System.Collections.Generic;

namespace GroupProjectBackend.Models.Dto
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int RouteId { get; set; }

        public int PlaceId { get; set; }

        public bool IsFavourite { get; set; }

        //public bool IsAddedByThisUser { get; set; }

        public int UserRating { get; set; }

        public List<CommentDto> Comments = new List<CommentDto>();
    }
    
}
