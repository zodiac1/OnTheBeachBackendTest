using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Hotels;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.IntegrationTests
{
    public class HotelSearchTests
    {
        private IList<Hotel> TestHotelsData;

        [SetUp]
        public void Setup()
        {
            TestHotelsData = Helpers.GetTestHotelsData();
        }

        [Test]
        public void Search_TestHotel_ReturnsSortedHotelData()
        {
            //Arrange
            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "PMI", Nights = 14, ArrivalDate = new DateTime(2023, 6, 15) }
            };

            //Act
            var hotels = hotelSearch.Search();

            //Assert
            Assert.NotNull(hotels);
            Assert.True(hotels.Any());
            Assert.True(hotels.First().Id == 3);
            Assert.True(hotels.First().PricePerNight == 59);
        }

        [Test]
        public void Search_TestHotelCustomer1_ReturnsSortedHotelData()
        {
            //Arrange
            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "AGP", Nights = 7, ArrivalDate = new DateTime(2023, 7, 1) }
            };

            //Act
            var hotels = hotelSearch.Search();

            //Assert
            Assert.NotNull(hotels);
            Assert.True(hotels.Any());
            Assert.True(hotels.First().Id == 9);
            Assert.True(hotels.First().LocalAirports.Contains("AGP"));
        }

        [Test]
        public void Search_TestHotelCustomer2_ReturnsSortedHotelData()
        {
            //Arrange
            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "PMI", Nights = 10, ArrivalDate = new DateTime(2023, 6, 15) }
            };

            //Act
            var hotels = hotelSearch.Search();

            //Assert
            Assert.NotNull(hotels);
            Assert.True(hotels.Any());
            Assert.True(hotels.First().Id == 5);
            Assert.True(hotels.First().LocalAirports.Contains("PMI"));
        }

        [Test]
        public void Search_TestHotelCustomer3_ReturnsSortedHotelData()
        {
            //Arrange
            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "LPA", Nights = 14, ArrivalDate = new DateTime(2022, 11, 10) }
            };

            //Act
            var hotels = hotelSearch.Search();

            //Assert
            Assert.NotNull(hotels);
            Assert.True(hotels.Any());
            Assert.True(hotels.First().Id == 6);
            Assert.True(hotels.First().LocalAirports.Contains("LPA"));
        }
    }
}