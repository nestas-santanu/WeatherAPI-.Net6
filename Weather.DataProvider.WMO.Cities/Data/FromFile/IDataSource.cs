namespace Weather.DataProvider.WMO.Cities.Data.FromFile
{
    public interface IDataSource
    {
        string? FilePath { get; }
    }
}