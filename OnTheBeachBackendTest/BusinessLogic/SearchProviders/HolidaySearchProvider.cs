using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.SearchProviders;

namespace OnTheBeachBackendTest.BusinessLogic.SearchProviders
{
    public class HolidaySearchProvider
    {
        public Holiday? Search(ISearchProvider<Flight> flightSearch, ISearchProvider<Hotel> hotelSearch)
        {
            if (flightSearch == null)
            {
                throw new ArgumentNullException(nameof(flightSearch));
            }

            if (hotelSearch == null)
            {
                throw new ArgumentNullException(nameof(hotelSearch));
            }

            var flightSearchResults = flightSearch.Search();
            var hotelSearchResults = hotelSearch.Search();

            if (flightSearchResults != null &&
                flightSearchResults.Any() &&
                hotelSearchResults != null &&
                hotelSearchResults.Any())
            {
                return new Holiday { Flight = flightSearchResults.First(), Hotel = hotelSearchResults.First() };
            }

            return null;
        }
    }
}