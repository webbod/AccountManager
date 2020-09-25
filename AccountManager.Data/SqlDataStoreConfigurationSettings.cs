using AccountManager.Interfaces.DataStore;

namespace AccountManager.Data
{
    public class SqlDataStoreConfigurationSettings : ISetOfConfigrationSettings
    {
        public string ConnectionString { get; set; }
    }
}
