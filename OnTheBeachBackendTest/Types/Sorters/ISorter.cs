namespace OnTheBeachBackendTest.Types.Sorters
{
    public interface ISorter<T>
    {
        IEnumerable<T>? Sort(IEnumerable<T> items);
    }
}