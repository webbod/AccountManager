using AccountManager.Interfaces.DataStore;

namespace AccountManager.SqlDataStore
{
    public class SqlDataStoreConfigurationSettings : ISetOfConfigrationSettings
    {
        public string ConnectionString { get; set; }
    }
}
