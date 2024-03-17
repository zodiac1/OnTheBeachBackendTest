using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;

namespace OnTheBeachBackendTest.BusinessLogic.DataSources
{
    public class FromLondonFlightsData : IDataSource<Flight>
    {
        private readonly string[] LONDON_AIRPORT_CODES = ["LCY", "LHR", "LGW", "LTN", "STN", "SEN"];

        private IEnumerable<Flight>? _Flights;

        public required IEnumerable<Flight>? Flights
        {
            set
            {
                _Flights = value == null ? null : value.Where(flight =>
                                                                flight != null &&
                                                                !string.IsNullOrWhiteSpace(flight.From) &&
                                                                LONDON_AIRPORT_CODES.Any(code => code.Equals(flight.From, StringComparison.OrdinalIgnoreCase))
                                                              );
            }
        }

        public IEnumerable<Flight>? GetData()
        {
            return _Flights;
        }
    }
}