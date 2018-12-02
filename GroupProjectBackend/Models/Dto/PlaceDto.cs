
using System.Collections.Generic;
using GroupProjectBackend.Models.DB;

namespace GroupProjectBackend.Models.Dto
{
    public class PlaceDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }//
        public string Label { get; set; }
        public PositionDto Position { get; set; }
        public string Description { get; set; }
        public float AverageRating { get; set; }
        public bool IsPublic { get; set;}
        public string FullAddress { get; set; }
        public CategoryDto Category { get; set; } //TODO: refactor this
        //public RatingDto Rating { get; set; }
        //public List<CategoryDto> AllCategories = new List<CategoryDto>();
    }

    public class PositionDto
    {
        public float Lat { get; set; }
        public float Lng { get; set; }
    }
}
