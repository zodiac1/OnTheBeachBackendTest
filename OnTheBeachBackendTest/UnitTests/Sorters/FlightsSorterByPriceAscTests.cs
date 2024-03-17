using OnTheBeachBackendTest.BusinessLogic.DataSources;
using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Entities;
using System.Text.Json;

namespace OnTheBeachBackendTest.UnitTests.Sorters
{
    public class FlightsSorterByPriceAscTests
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
        public void Sort_FlightsByPriceAsc_ReturnsAllFlightsByPriceAsc()
        {
            //Arrange
            var allFlightsData = new AllFlightsData { Flights = TestFlights };
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
