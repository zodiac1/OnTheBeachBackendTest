using Moq;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;
using OnTheBeachBackendTest.Types.SearchPredicates;
using OnTheBeachBackendTest.Types.Sorters;

namespace OnTheBeachBackendTest.UnitTests.SearchProviders
{
    public class HolidaySearchProviderTests
    {
        private IList<Flight> TestFlightsData;
        private IList<Hotel> TestHotelsData;

        [SetUp]
        public void Setup()
        {
            TestFlightsData = Helpers.GetTestFlightsData();
            TestHotelsData = Helpers.GetTestHotelsData();
        }

        [Test]
        public void Search_TestHoliday_ReturnsHoliday()
        {
            //Arrange
            var flightDataSourceMock = new Mock<IDataSource<Flight>>();
            var flightSearchPredicateMock = new Mock<ISearchPredicate<Flight>>();
            var flightSorterMock = new Mock<ISorter<Flight>>();

            var hotelDataSourceMock = new Mock<IDataSource<Hotel>>();
            var hotelSearchPredicateMock = new Mock<ISearchPredicate<Hotel>>();
            var hotelSorterMock = new Mock<ISorter<Hotel>>();

            var testFlight1 = TestFlightsData[1];
            var testHotel1 = TestHotelsData[1];

            flightDataSourceMock.Setup(x => x.GetData())
                .Returns(TestFlightsData);

            flightSearchPredicateMock.Setup(x => x.IsMatch(testFlight1))
                .Returns(true);

            flightSorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Flight>>()))
                .Returns(TestFlightsData);

            hotelDataSourceMock.Setup(x => x.GetData())
                .Returns(TestHotelsData);

            hotelSearchPredicateMock.Setup(x => x.IsMatch(testHotel1))
                .Returns(true);

            hotelSorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Hotel>>()))
                .Returns(TestHotelsData);

            var flightDataSource = flightDataSourceMock.Object;
            var flightSearchPredicate = flightSearchPredicateMock.Object;
            var flightSorter = flightSorterMock.Object;

            var flightSearchProvider = new SearchProvider<Flight> { DataSource = flightDataSource, SearchPredicate = flightSearchPredicate, Sorter = flightSorter };

            var hotelDataSource = hotelDataSourceMock.Object;
            var hotelSearchPredicate = hotelSearchPredicateMock.Object;
            var hotelSorter = hotelSorterMock.Object;

            var hotelSearchProvider = new SearchProvider<Hotel> { DataSource = hotelDataSource, SearchPredicate = hotelSearchPredicate, Sorter = hotelSorter };

            //Act
            var holidayResults = new HolidaySearchProvider().Search(flightSearchProvider, hotelSearchProvider);

            //Assert
            Assert.IsNotNull(holidayResults);
        }
    }
}