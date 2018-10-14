namespace GroupProjectBackend.Config
{
    using Microsoft.Extensions.Configuration;

    public class ConfigurationProvider : IConfigurationProvider
    {
        private readonly IConfiguration _config;

        public ConfigurationProvider(IConfiguration config)
        {
            _config = config;
        }

        public string ConnectionString => _config.GetSection("ConnectionString").Value;
    }
}
