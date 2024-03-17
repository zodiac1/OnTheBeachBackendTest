using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.Sorters;

namespace OnTheBeachBackendTest.BusinessLogic.Sorters
{
    public class HotelsSorterByPriceAsc : ISorter<Hotel>
    {
        public IEnumerable<Hotel> Sort(IEnumerable<Hotel> hotels)
        {
            return hotels.OrderBy(hotel => hotel.PricePerNight);
        }
    }
}