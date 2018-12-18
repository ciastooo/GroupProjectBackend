using System.Collections.Generic;

namespace GroupProjectBackend.Models.Dto
{
    public class RouteDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }
        public float AverageRating { get; set; }

        public IList<PlaceDto> Places { get; set; }
    }
}
