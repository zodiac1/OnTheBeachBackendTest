using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Flights;
using OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Hotels;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.IntegrationTests
{
    public class HolidaySearchTests
    {
        private IList<Flight> TestFlightsData;
        private IList<Hotel> TestHotelsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
            TestHotelsData = Helpers.GetTestHotelsData();
        }

        [Test]
        public void Search_TestHolidayCustomer1_ReturnsHolidayMatch()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new AllFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToFromDepartureExactSearchPredicate { From = "MAN", To = "AGP", DepartureDate = new DateTime(2023, 7, 1) }
            };

            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "AGP", Nights = 7, ArrivalDate = new DateTime(2023, 7, 1) }
            };

            //Act
            var holiday = new HolidaySearchProvider().Search(flightSearch, hotelSearch);

            //Assert
            Assert.IsNotNull(holiday);
            Assert.IsNotNull(holiday.Flight);
            Assert.IsNotNull(holiday.Hotel);
            Assert.True(holiday.Flight.Id == 2);
            Assert.True(holiday.Flight.From == "MAN");
            Assert.True(holiday.Flight.To == "AGP");
            Assert.True(holiday.Hotel.Id == 9);
            Assert.True(holiday.Hotel.LocalAirports.Contains("AGP"));
        }

        [Test]
        public void Search_TestHolidayCustomer2_ReturnsHolidayMatch()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new FromLondonFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToDepartureExactSearchPredicate { To = "PMI", DepartureDate = new DateTime(2023, 6, 15) }
            };

            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "PMI", Nights = 10, ArrivalDate = new DateTime(2023, 6, 15) }
            };

            //Act
            var holiday = new HolidaySearchProvider().Search(flightSearch, hotelSearch);

            //Assert
            Assert.IsNotNull(holiday);
            Assert.IsNotNull(holiday.Flight);
            Assert.IsNotNull(holiday.Hotel);
            Assert.True(holiday.Flight.Id == 6);
            Assert.True(holiday.Flight.From == "LGW");
            Assert.True(holiday.Flight.To == "PMI");
            Assert.True(holiday.Hotel.Id == 5);
            Assert.True(holiday.Hotel.LocalAirports.Contains("PMI"));
        }

        [Test]
        public void Search_TestHolidayCustomer3_ReturnsHolidayMatch()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new AllFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToDepartureExactSearchPredicate { To = "LPA", DepartureDate = new DateTime(2022, 11, 10) }
            };

            var hotelSearch = new SearchProvider<Hotel>
            {
                DataSource = new AllHotelsData { Hotels = TestHotelsData },
                Sorter = new HotelsSorterByPriceAsc(),
                SearchPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = "LPA", Nights = 14, ArrivalDate = new DateTime(2022, 11, 10) }
            };

            //Act
            var holiday = new HolidaySearchProvider().Search(flightSearch, hotelSearch);

            //Assert
            Assert.IsNotNull(holiday);
            Assert.IsNotNull(holiday.Flight);
            Assert.IsNotNull(holiday.Hotel);
            Assert.True(holiday.Flight.Id == 7);
            Assert.True(holiday.Flight.From == "MAN");
            Assert.True(holiday.Flight.To == "LPA");
            Assert.True(holiday.Hotel.Id == 6);
            Assert.True(holiday.Hotel.LocalAirports.Contains("LPA"));
        }
    }
}