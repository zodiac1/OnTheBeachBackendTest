using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.Entities;
using System.Text.Json;

namespace OnTheBeachBackendTest.UnitTests.DataSources
{
    public class FromLondonFlightsDataTests
    {
        private readonly string[] LONDON_AIRPORT_CODES = ["LCY", "LHR", "LGW", "LTN", "STN", "SEN"];

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
        public void GetData_FromJson_ReturnsLondonFlightsOnly()
        {
            //Arrange
            var londonFlightsData = new FromLondonFlightsData { Flights = TestFlights };

            //Act
            var londonFlights = londonFlightsData.GetData();

            //Assert
            Assert.True(londonFlights.Any());
            Assert.True(londonFlights.Count() == 4);
            Assert.True(londonFlights.All(flight => LONDON_AIRPORT_CODES.Any(code => code.Equals(flight.From, StringComparison.OrdinalIgnoreCase))));
        }
    }
}