using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;

namespace OnTheBeachBackendTest.BusinessLogic.DataSources
{
    public class AllFlightsData : IDataSource<Flight>
    {
        public required IEnumerable<Flight> Flights { private get; set; }

        public IEnumerable<Flight>? GetData()
        {
            return Flights;
        }
    }
}