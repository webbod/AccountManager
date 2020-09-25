using AccountManager.Interfaces.DataStore;
using System.ComponentModel;

namespace AccountManager.Interfaces.DataStore
{
    public interface IInserter<TConfiguration, TOutput> : IInitialisable
    { 
        TOutput Insert(TConfiguration configuration);
    }
}