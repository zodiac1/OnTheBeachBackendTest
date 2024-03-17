using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.UnitTests.Sorters
{
    public class FlightsSorterByPriceAscTests
    {
        private IList<Flight>? TestFlightsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
        }

        [Test]
        public void Sort_FlightsByPriceAsc_ReturnsAllFlightsByPriceAsc()
        {
            //Arrange
            var allFlightsData = new AllFlightsData { Flights = TestFlightsData };
            var allFlights = allFlightsData.GetData();
            var flightsSorterByPriceAsc = new FlightsSorterByPriceAsc();

            //Act
            var sortedFlights = flightsSorterByPriceAsc.Sort(allFlights);

            //Assert
            Assert.True(sortedFlights.Any());
            Assert.True(sortedFlights.Count() == 12);
            Assert.True(sortedFlights.First().Id == 6);
            Assert.True(sortedFlights.First().Price == 75);
        }
    }
}
