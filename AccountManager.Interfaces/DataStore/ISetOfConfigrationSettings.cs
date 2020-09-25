namespace AccountManager.Interfaces.DataStore
{
    /// <summary>
    /// Describes settings to configure a data store
    /// </summary>
    public interface ISetOfConfigrationSettings
    {
        string ConnectionString { get; }
    }
}
