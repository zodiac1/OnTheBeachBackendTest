using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.UnitTests.DataSources
{
    public class AllFlightsDataTests
    {
        private IList<Flight>? TestFlightsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
        }

        [Test]
        public void GetData_FromJson_ReturnsAllFlights()
        {
            //Arrange
            var allFlightsData = new AllFlightsData { Flights = TestFlightsData };

            //Act
            var allFlights = allFlightsData.GetData();

            //Assert
            Assert.True(allFlights.Any());
            Assert.True(allFlights.Count() == 12);
        }
    }
}