using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.Accounts.Repository
{
    public interface IAccountInserter : IInserter<IAccountCredentials, IAccount>
    {
    }
}