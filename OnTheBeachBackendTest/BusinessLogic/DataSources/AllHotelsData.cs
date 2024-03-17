using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;

namespace OnTheBeachBackendTest.BusinessLogic.DataSources
{
    public class AllHotelsData : IDataSource<Hotel>
    {
        public required IEnumerable<Hotel> Hotels { private get; set; }

        public IEnumerable<Hotel>? GetData()
        {
            return Hotels;
        }
    }
}