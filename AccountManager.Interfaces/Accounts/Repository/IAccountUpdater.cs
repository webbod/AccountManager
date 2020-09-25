using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.Accounts.Repository
{
    public interface IAccountUpdater : IUpdater<IAccountCredentials, IAccount>
    {
    }
}