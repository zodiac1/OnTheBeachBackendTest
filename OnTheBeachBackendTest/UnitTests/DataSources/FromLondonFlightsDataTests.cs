using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.UnitTests.DataSources
{
    public class FromLondonFlightsDataTests
    {
        private readonly string[] LONDON_AIRPORT_CODES = ["LCY", "LHR", "LGW", "LTN", "STN", "SEN"];

        private IList<Flight>? TestFlightsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
        }

        [Test]
        public void GetData_FromJson_ReturnsLondonFlightsOnly()
        {
            //Arrange
            var londonFlightsData = new FromLondonFlightsData { Flights = TestFlightsData };

            //Act
            var londonFlights = londonFlightsData.GetData();

            //Assert
            Assert.True(londonFlights.Any());
            Assert.True(londonFlights.Count() == 4);
            Assert.True(londonFlights.All(flight => LONDON_AIRPORT_CODES.Any(code => code.Equals(flight.From, StringComparison.OrdinalIgnoreCase))));
        }
    }
}