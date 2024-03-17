namespace OnTheBeachBackendTest.Types.DataSources
{
    public interface IDataSource<T>
    {
        IEnumerable<T>? GetData();
    }
}