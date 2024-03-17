using Moq;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;
using OnTheBeachBackendTest.Types.SearchPredicates;
using OnTheBeachBackendTest.Types.Sorters;
using System.Text.Json;

namespace OnTheBeachBackendTest.UnitTests.SearchProviders
{
    public class FlightSearchProviderTests
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
        public void Search_AllTestFlights_ReturnsAllTestFlights()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Flight>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Flight>>();
            var sorterMock = new Mock<ISorter<Flight>>();

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestFlights);

            searchPredicateMock.Setup(x => x.IsMatch(It.IsAny<Flight>()))
                .Returns(true);

            sorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Flight>>()))
                .Returns(TestFlights);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Flight> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allFlights = searchProvider.Search();

            //Assert
            Assert.True(allFlights.Any());
            Assert.True(allFlights.Count() == 12);
        }

        [Test]
        public void Search_NoMatchingFlights_ReturnsNull()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Flight>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Flight>>();
            var sorterMock = new Mock<ISorter<Flight>>();

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestFlights);

            searchPredicateMock.Setup(x => x.IsMatch(It.IsAny<Flight>()))
                .Returns(false);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Flight> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allFlights = searchProvider.Search();

            //Assert
            Assert.IsNull(allFlights);
        }

        [Test]
        public void Search_NoFlights_ReturnsNull()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Flight>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Flight>>();
            var sorterMock = new Mock<ISorter<Flight>>();

            dataSourceMock.Setup(ds => ds.GetData())
                .Returns((IEnumerable<Flight>?)null);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Flight> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allFlights = searchProvider.Search();

            //Assert
            Assert.IsNull(allFlights);
        }

        [Test]
        public void Search_SpecificFlights_CallsSortOnSpecifiedFlightsOnce()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Flight>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Flight>>();
            var sorterMock = new Mock<ISorter<Flight>>();

            var testFlight1 = TestFlights[1];
            var testFlight2 = TestFlights[2];

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestFlights);

            searchPredicateMock.Setup(x => x.IsMatch(testFlight1))
                .Returns(true);

            searchPredicateMock.Setup(x => x.IsMatch(testFlight2))
                .Returns(true);

            sorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Flight>>()))
                .Returns(TestFlights);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Flight> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allFlights = searchProvider.Search();

            //Assert
            sorterMock.Verify(x => x.Sort(new List<Flight> { testFlight1, testFlight2 }), Times.Once());
        }
    }
}