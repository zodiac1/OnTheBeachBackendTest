using OnTheBeachBackendTest.Types.DataSources;
using OnTheBeachBackendTest.Types.SearchPredicates;
using OnTheBeachBackendTest.Types.SearchProviders;
using OnTheBeachBackendTest.Types.Sorters;

namespace OnTheBeachBackendTest.BusinessLogic.SearchProviders
{
    public class SearchProvider<T> : ISearchProvider<T>
    {
        public required IDataSource<T> DataSource { get; set; }
        public required ISorter<T> Sorter { get; set; }
        public required ISearchPredicate<T> SearchPredicate { get; set; }

        public virtual IEnumerable<T>? Search()
        {
            var searchData = DataSource.GetData();

            if (searchData == null ||
                searchData.Count() == 0)
            {
                return null;
            }

            var searchResult = searchData.Where(x => SearchPredicate.IsMatch(x));

            if (searchResult == null ||
                searchResult.Count() == 0)
            {
                return null;
            }

            return Sorter.Sort(searchResult);
        }
    }
}