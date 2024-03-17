using OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Flights;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.UnitTests.SearchPredicates.Flights
{
    public class FlightToFromDepartureExactSearchPredicateTests
    {
        private IList<Flight>? TestFlightsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
        }

        [Test]
        public void IsMatch_TestFlight_ReturnsTrue()
        {
            //Arrange
            var testFlight = TestFlightsData[1];
            var flightPredicate = new FlightToFromDepartureExactSearchPredicate { DepartureDate = testFlight.DepartureDate, To = testFlight.To, From = testFlight.From };

            //Act
            var result = flightPredicate.IsMatch(testFlight);

            //Assert
            Assert.True(result);
        }

        [Test]
        public void IsMatch_FakeFlight_ReturnsFalse()
        {
            //Arrange
            var testFlight = TestFlightsData[1];
            var fakeFlight = new Flight { Airline = "", DepartureDate = DateTime.Now, From = "", Id = 0, Price = 0, To = "" };
            var flightPredicate = new FlightToFromDepartureExactSearchPredicate { DepartureDate = testFlight.DepartureDate, To = testFlight.To, From = testFlight.From };

            //Act
            var result = flightPredicate.IsMatch(fakeFlight);

            //Assert
            Assert.False(result);
        }
    }
}
