using Moq;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.Data;
using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;
using OnTheBeachBackendTest.Types.SearchPredicates;
using OnTheBeachBackendTest.Types.Sorters;

namespace OnTheBeachBackendTest.UnitTests.SearchProviders
{
    public class HotelSearchProviderTests
    {
        private IList<Hotel> TestHotelsData;

        [SetUp]
        public void Setup()
        {
            TestHotelsData = Helpers.GetTestHotelsData();
        }

        [Test]
        public void Search_AllTestHotels_ReturnsAllTestHotels()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Hotel>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Hotel>>();
            var sorterMock = new Mock<ISorter<Hotel>>();

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestHotelsData);

            searchPredicateMock.Setup(x => x.IsMatch(It.IsAny<Hotel>()))
                .Returns(true);

            sorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Hotel>>()))
                .Returns(TestHotelsData);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Hotel> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allHotels = searchProvider.Search();

            //Assert
            Assert.IsNotNull(allHotels);
            Assert.True(allHotels.Any());
            Assert.True(allHotels.Count() == 13);
        }

        [Test]
        public void Search_NoMatchingHotels_ReturnsNull()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Hotel>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Hotel>>();
            var sorterMock = new Mock<ISorter<Hotel>>();

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestHotelsData);

            searchPredicateMock.Setup(x => x.IsMatch(It.IsAny<Hotel>()))
                .Returns(false);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Hotel> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allHotels = searchProvider.Search();

            //Assert
            Assert.IsNull(allHotels);
        }

        [Test]
        public void Search_NoHotels_ReturnsNull()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Hotel>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Hotel>>();
            var sorterMock = new Mock<ISorter<Hotel>>();

            dataSourceMock.Setup(ds => ds.GetData())
                .Returns((IEnumerable<Hotel>?)null);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Hotel> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allHotels = searchProvider.Search();

            //Assert
            Assert.IsNull(allHotels);
        }

        [Test]
        public void Search_SpecificHotels_CallsSortOnSpecifiedHotelsOnce()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Hotel>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Hotel>>();
            var sorterMock = new Mock<ISorter<Hotel>>();

            var testHotel1 = TestHotelsData[1];
            var testHotel2 = TestHotelsData[2];

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestHotelsData);

            searchPredicateMock.Setup(x => x.IsMatch(testHotel1))
                .Returns(true);

            searchPredicateMock.Setup(x => x.IsMatch(testHotel2))
                .Returns(true);

            sorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Hotel>>()))
                .Returns(TestHotelsData);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Hotel> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allHotels = searchProvider.Search();

            //Assert
            sorterMock.Verify(x => x.Sort(new List<Hotel> { testHotel1, testHotel2 }), Times.Once());
        }
    }
}