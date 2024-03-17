using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.Sorters;

namespace OnTheBeachBackendTest.BusinessLogic.Sorters
{
    public class FlightsSorterByPriceAsc : ISorter<Flight>
    {
        public IEnumerable<Flight> Sort(IEnumerable<Flight> flights)
        {
            return flights.OrderBy(flight => flight.Price);
        }
    }
}