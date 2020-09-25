using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.DataStore
{

    public interface IDeleter<TCriteria> : IInitialisable
    {
        int Delete(TCriteria criteria, bool definitelyDeleteIt = false);
    }
}