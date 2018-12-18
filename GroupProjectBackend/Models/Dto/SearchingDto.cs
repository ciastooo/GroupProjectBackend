namespace GroupProjectBackend.Models.Dto
{
    public class SearchingDto
    {
        public string Label { get; set; }
        public int AverageRating { get; set; }
    }

    public class PlaceSearchingDto : SearchingDto
    {
        public string FullAddress { get; set; }
        public int CategoryId { get; set; }
        public float Distance { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
