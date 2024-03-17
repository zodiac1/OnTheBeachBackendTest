using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Flights;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.IntegrationTests
{
    public class FlightSearchTests
    {
        private IList<Flight>? TestFlightsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
        }

        [Test]
        public void Search_TestFlight_ReturnsSortedFlightData()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new AllFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToFromDepartureExactSearchPredicate { From = "MAN", To = "PMI", DepartureDate = new DateTime(2023, 6, 15) }
            };

            //Act
            var flight = flightSearch.Search();

            //Assert
            Assert.NotNull(flight);
            Assert.True(flight.Any());
            Assert.True(flight.First().Id == 5);
            Assert.True(flight.First().From == "MAN");
            Assert.True(flight.First().Price == 130);
        }

        [Test]
        public void Search_TestFlightCustomer1_ReturnsSortedFlightData()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new AllFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToFromDepartureExactSearchPredicate { From = "MAN", To = "AGP", DepartureDate = new DateTime(2023, 7, 1) }
            };

            //Act
            var flight = flightSearch.Search();

            //Assert
            Assert.NotNull(flight);
            Assert.True(flight.Any());
            Assert.True(flight.First().Id == 2);
            Assert.True(flight.First().From == "MAN");
            Assert.True(flight.First().To == "AGP");
        }

        [Test]
        public void Search_TestFlightCustomer2_ReturnsSortedFlightData()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new FromLondonFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToDepartureExactSearchPredicate { To = "PMI", DepartureDate = new DateTime(2023, 6, 15) }
            };

            //Act
            var flight = flightSearch.Search();

            //Assert
            Assert.NotNull(flight);
            Assert.True(flight.Any());
            Assert.True(flight.First().Id == 6);
            Assert.True(flight.First().From == "LGW");
            Assert.True(flight.First().To == "PMI");
        }

        [Test]
        public void Search_TestFlightCustomer3_ReturnsSortedFlightData()
        {
            //Arrange
            var flightSearch = new SearchProvider<Flight>
            {
                DataSource = new AllFlightsData { Flights = TestFlightsData },
                Sorter = new FlightsSorterByPriceAsc(),
                SearchPredicate = new FlightToDepartureExactSearchPredicate { To = "LPA", DepartureDate = new DateTime(2022, 11, 10) }
            };

            //Act
            var flight = flightSearch.Search();

            //Assert
            Assert.NotNull(flight);
            Assert.True(flight.Any());
            Assert.True(flight.First().Id == 7);
            Assert.True(flight.First().From == "MAN");
            Assert.True(flight.First().To == "LPA");
        }
    }
}