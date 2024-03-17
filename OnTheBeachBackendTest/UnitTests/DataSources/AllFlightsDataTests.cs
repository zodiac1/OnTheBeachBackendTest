using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.Entities;
using System.Text.Json;

namespace OnTheBeachBackendTest.UnitTests.DataSources
{
    public class AllFlightsDataTests
    {
        private IList<Flight>? TestFlights;

        [SetUp]
        public void Setup()
        {
            using (var reader = new StreamReader("flight-data.json"))
            {
                var json = reader.ReadToEnd();
                var flightsRaw = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(json);

                TestFlights = new List<Flight>();

                foreach (var item in flightsRaw)
                {
                    TestFlights.Add(new Flight { Id = item["id"].GetInt32(), Airline = item["airline"].GetString(), From = item["from"].GetString(), To = item["to"].GetString(), Price = item["price"].GetDouble(), DepartureDate = DateTime.Parse(item["departure_date"].GetString()) });
                }
            }
        }

        [Test]
        public void GetData_FromJson_ReturnsAllFlights()
        {
            //Arrange
            var allFlightsData = new AllFlightsData { Flights = TestFlights };

            //Act
            var allFlights = allFlightsData.GetData();

            //Assert
            Assert.True(allFlights.Any());
            Assert.True(allFlights.Count() == 12);
        }
    }
}