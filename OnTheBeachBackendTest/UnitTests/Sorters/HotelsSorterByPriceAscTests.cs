using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Entities;
using System.Text.Json;

namespace OnTheBeachBackendTest.UnitTests.Sorters
{
    public class HotelsSorterByPriceAscTests
    {
        private IList<Hotel>? TestHotels;

        [SetUp]
        public void Setup()
        {
            using (var reader = new StreamReader("hotel-data.json"))
            {
                var json = reader.ReadToEnd();
                var hotelsRaw = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(json);

                TestHotels = new List<Hotel>();

                foreach (var item in hotelsRaw)
                {
                    JsonElement localAirportsRaw = item["local_airports"];
                    string[] localAirports = new string[localAirportsRaw.GetArrayLength()];
                    var enumerator = localAirportsRaw.EnumerateArray();

                    var i = 0;

                    while (enumerator.MoveNext())
                    {
                        localAirports[i++] = enumerator.Current.GetString()!;
                    }


                    TestHotels.Add(new Hotel { Id = item["id"].GetInt32(), Name = item["name"].GetString(), PricePerNight = item["price_per_night"].GetDouble(), ArrivalDate = DateTime.Parse(item["arrival_date"].GetString()), LocalAirports = localAirports, Nights = item["nights"].GetInt32() });
                }
            }
        }

        [Test]
        public void Sort_HotelsSorterByPriceAsc_ReturnsAllHotelsByPriceAsc()
        {
            //Arrange
            var hotelsSorterByPriceAsc = new HotelsSorterByPriceAsc();

            //Act
            var sortedHotels = hotelsSorterByPriceAsc.Sort(TestHotels);

            //Assert
            Assert.True(sortedHotels.Any());
            Assert.True(sortedHotels.Count() == 13);
            Assert.True(sortedHotels.First().Id == 8);
            Assert.True(sortedHotels.First().PricePerNight == 45);
        }
    }
}
