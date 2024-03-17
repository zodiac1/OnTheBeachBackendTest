using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.SearchPredicates;

namespace OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Hotels
{
    public class HotelAirportArrivalNightsExactSearchPredicate : ISearchPredicate<Hotel>
    {
        public required string Airport { get; set; }
        public required int Nights { get; set; }
        public required DateTime ArrivalDate { get; set; }

        public bool IsMatch(Hotel hotel)
        {
            return
                hotel != null &&
                DateTime.Compare(hotel.ArrivalDate, ArrivalDate) == 0 &&
                hotel.Nights == Nights &&
                hotel.LocalAirports != null &&
                hotel.LocalAirports.Any(localAirport => localAirport.Equals(Airport, StringComparison.OrdinalIgnoreCase))
            ;

        }
    }
}
