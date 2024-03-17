using Moq;
using OnTheBeachBackendTest.BusinessLogic.SearchProviders;
using OnTheBeachBackendTest.Entities;
using OnTheBeachBackendTest.Types.DataSources;
using OnTheBeachBackendTest.Types.SearchPredicates;
using OnTheBeachBackendTest.Types.Sorters;
using System.Text.Json;

namespace OnTheBeachBackendTest.UnitTests.SearchProviders
{
    public class HotelSearchProviderTests
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
        public void Search_AllTestHotels_ReturnsAllTestHotels()
        {
            //Arrange
            var dataSourceMock = new Mock<IDataSource<Hotel>>();
            var searchPredicateMock = new Mock<ISearchPredicate<Hotel>>();
            var sorterMock = new Mock<ISorter<Hotel>>();

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestHotels);

            searchPredicateMock.Setup(x => x.IsMatch(It.IsAny<Hotel>()))
                .Returns(true);

            sorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Hotel>>()))
                .Returns(TestHotels);

            var dataSource = dataSourceMock.Object;
            var searchPredicate = searchPredicateMock.Object;
            var sorter = sorterMock.Object;

            var searchProvider = new SearchProvider<Hotel> { DataSource = dataSource, SearchPredicate = searchPredicate, Sorter = sorter };

            //Act
            var allHotels = searchProvider.Search();

            //Assert
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
                .Returns(TestHotels);

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

            var testHotel1 = TestHotels[1];
            var testHotel2 = TestHotels[2];

            dataSourceMock.Setup(x => x.GetData())
                .Returns(TestHotels);

            searchPredicateMock.Setup(x => x.IsMatch(testHotel1))
                .Returns(true);

            searchPredicateMock.Setup(x => x.IsMatch(testHotel2))
                .Returns(true);

            sorterMock.Setup(x => x.Sort(It.IsAny<IEnumerable<Hotel>>()))
                .Returns(TestHotels);

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