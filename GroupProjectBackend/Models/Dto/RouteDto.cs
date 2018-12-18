using System.Collections.Generic;

namespace GroupProjectBackend.Models.Dto
{
    public class RouteDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public double AverageRating { get; set; }

        public IList<PositionDto> Places { get; set; }
    }
}
