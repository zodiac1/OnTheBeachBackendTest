using OnTheBeachBackendTest.Entities;
using System.Text.Json;

namespace OnTheBeachBackendTest.Data
{
    public static class Helpers
    {
        public static IList<Flight>? GetTestFlightsData()
        {
            using (var reader = new StreamReader("flight-data.json"))
            {
                var json = reader.ReadToEnd();
                var flightsRaw = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(json);

                var testFlights = new List<Flight>();

                foreach (var item in flightsRaw)
                {
                    testFlights.Add(new Flight { Id = item["id"].GetInt32(), Airline = item["airline"].GetString(), From = item["from"].GetString(), To = item["to"].GetString(), Price = item["price"].GetDouble(), DepartureDate = DateTime.Parse(item["departure_date"].GetString()) });
                }

                return testFlights;
            }
        }

        public static IList<Hotel>? GetTestHotelsData()
        {
            using (var reader = new StreamReader("hotel-data.json"))
            {
                var json = reader.ReadToEnd();
                var hotelsRaw = JsonSerializer.Deserialize<List<Dictionary<string, dynamic>>>(json);

                var testHotels = new List<Hotel>();

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


                    testHotels.Add(new Hotel { Id = item["id"].GetInt32(), Name = item["name"].GetString(), PricePerNight = item["price_per_night"].GetDouble(), ArrivalDate = DateTime.Parse(item["arrival_date"].GetString()), LocalAirports = localAirports, Nights = item["nights"].GetInt32() });
                }

                return testHotels;
            }
        }
    }
}