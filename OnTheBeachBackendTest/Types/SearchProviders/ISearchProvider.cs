namespace OnTheBeachBackendTest.Types.SearchProviders
{
    public interface ISearchProvider<T>
    {
        public IEnumerable<T>? Search();
    }
}