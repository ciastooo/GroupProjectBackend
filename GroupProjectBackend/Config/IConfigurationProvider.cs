namespace GroupProjectBackend.Config
{
    public interface IConfigurationProvider
    {
        string ConnectionString { get; }
    }
}