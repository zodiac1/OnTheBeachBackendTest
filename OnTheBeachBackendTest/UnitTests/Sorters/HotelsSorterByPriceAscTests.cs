using OnTheBeachBackendTest.BusinessLogic.Sorters;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;

namespace OnTheBeachBackendTest.UnitTests.Sorters
{
    public class HotelsSorterByPriceAscTests
    {
        private IList<Hotel> TestHotelsData;

        [SetUp]
        public void Setup()
        {
            TestHotelsData = Helpers.GetTestHotelsData();
        }

        [Test]
        public void Sort_HotelsSorterByPriceAsc_ReturnsAllHotelsByPriceAsc()
        {
            //Arrange
            var hotelsSorterByPriceAsc = new HotelsSorterByPriceAsc();

            //Act
            var sortedHotels = hotelsSorterByPriceAsc.Sort(TestHotelsData);

            //Assert
            Assert.True(sortedHotels.Any());
            Assert.True(sortedHotels.Count() == 13);
            Assert.True(sortedHotels.First().Id == 8);
            Assert.True(sortedHotels.First().PricePerNight == 45);
        }
    }
}
