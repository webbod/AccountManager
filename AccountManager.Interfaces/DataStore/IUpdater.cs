using AccountManager.Interfaces.DataStore;
using System.ComponentModel;

namespace AccountManager.Interfaces.DataStore
{
    public interface IUpdater<TConfiguration, TOutput> : IInitialisable
    {
        TOutput Update(TConfiguration configuration);
    }
}