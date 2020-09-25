using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.DataStore
{

    public interface IFinder<TCriteria, TOutput> : IInitialisable
    {
        TOutput Find(TCriteria criteria);
    }
}