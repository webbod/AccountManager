using AccountManager.Interfaces.DataStore;
using System.ComponentModel;

namespace AccountManager.Interfaces.Accounts.Repository
{
    public interface IAccountUpdater
    {
        void Initalise(ISetOfConfigrationSettings options);

        IAccount Update(IAccountCredentials credentials);
    }
}