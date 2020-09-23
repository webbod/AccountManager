using System.Data;

namespace AccountManager.Interfaces.DataStore
{
    /// <summary>
    /// Describes a way to mapp from a dataRecord to an object
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IDataMapper<TEntity>
        where TEntity : class
    {
        TEntity Map(IDataRecord record);
    }
}