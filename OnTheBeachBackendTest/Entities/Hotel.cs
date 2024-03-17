namespace OnTheBeachBackendTest.Entities
{
    public class Hotel
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required DateTime ArrivalDate { get; set; }
        public required double PricePerNight { get; set; }
        public required string[] LocalAirports { get; set; }
        public required int Nights { get; set; }
    }
}