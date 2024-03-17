namespace OnTheBeachBackendTest.Types.SearchPredicates
{
    public interface ISearchPredicate<T>
    {
        public bool IsMatch(T t);
    }
}