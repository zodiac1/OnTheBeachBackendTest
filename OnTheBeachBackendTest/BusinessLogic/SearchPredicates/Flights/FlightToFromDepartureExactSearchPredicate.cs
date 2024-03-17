using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.SearchPredicates;

namespace OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Flights
{
    public class FlightToFromDepartureExactSearchPredicate : ISearchPredicate<Flight>
    {
        public required string To { get; set; }
        public required string From { get; set; }
        public required DateTime DepartureDate { get; set; }

        public bool IsMatch(Flight flight)
        {
            return
                flight != null &&
                string.Equals(flight.To, To, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(flight.From, From, StringComparison.OrdinalIgnoreCase) &&
                DateTime.Compare(flight.DepartureDate, DepartureDate) == 0
            ;

        }
    }
}