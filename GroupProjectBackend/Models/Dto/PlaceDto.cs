namespace GroupProjectBackend.Models.Dto
{
    public class PlaceDto
    {
        public string label { get; set; }
        public PositionDto position { get; set; }
        public string description { get; set; }
        public float rating { get; set; }
        public bool isPublic { get; set;}
        public string address { get; set; }
        public string category { get; set; } //TODO: refactor this
    }

    public class PositionDto
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }
}
