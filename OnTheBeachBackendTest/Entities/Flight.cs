namespace OnTheBeachBackendTest.Entities
{
    public class Flight
    {
        public required int Id { get; set; }
        public required string Airline { get; set; }
        public required string From { get; set; }
        public required string To { get; set; }
        public required double Price { get; set; }
        public required DateTime DepartureDate { get; set; }
    }
}