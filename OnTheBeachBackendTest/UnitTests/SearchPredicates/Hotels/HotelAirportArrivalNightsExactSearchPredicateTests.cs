using OnTheBeachBackendTest.BusinessLogic.SearchPredicates.Hotels;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.UnitTests.SearchPredicates.Hotels
{
    public class HotelAirportArrivalNightsExactSearchPredicateTests
    {
        private IList<Hotel> TestHotelsData;

        [SetUp]
        public void Setup()
        {
            TestHotelsData = Helpers.GetTestHotelsData();
        }

        [Test]
        public void IsMatch_TestHotel_ReturnsTrue()
        {
            //Arrange
            var testHotel = TestHotelsData[1];
            var hotelPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = testHotel.LocalAirports[0], ArrivalDate = testHotel.ArrivalDate, Nights = testHotel.Nights };

            //Act
            var result = hotelPredicate.IsMatch(testHotel);

            //Assert
            Assert.True(result);
        }

        [Test]
        public void IsMatch_FakeHotel_ReturnsFalse()
        {
            //Arrange
            var testHotel = TestHotelsData[1];
            var fakeHotel = new Hotel { Id = 0, ArrivalDate = DateTime.Now, LocalAirports = [], Name = "", Nights = 1, PricePerNight = 100 };
            var hotelPredicate = new HotelAirportArrivalNightsExactSearchPredicate { Airport = testHotel.LocalAirports[0], ArrivalDate = testHotel.ArrivalDate, Nights = testHotel.Nights };

            //Act
            var result = hotelPredicate.IsMatch(fakeHotel);

            //Assert
            Assert.False(result);
        }
    }
}
