using System.Collections.Generic;

namespace GroupProjectBackend.Models.Dto
{
    public class RouteDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public bool IsPublic { get; set; }

        public IList<PlaceDto> Places { get; set; }
    }
}
