namespace Weather.DataProvider.WMO.Resource
{
    /// <summary>
    /// Acknowledgement of the source of data
    /// </summary>
    public interface IDataProvider
    {
        string Name { get; }
        string Website { get; }
    }
}