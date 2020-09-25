using AccountManager.Interfaces.DataStore;
using System.ComponentModel;

namespace AccountManager.Interfaces.Accounts.Repository
{
    public interface IAccountInserter
    {
        void Initalise(ISetOfConfigrationSettings options);

        IAccount Insert(IAccountCredentials credentials);
    }
}