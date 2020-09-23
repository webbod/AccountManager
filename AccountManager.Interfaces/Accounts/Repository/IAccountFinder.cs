using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.Accounts.Repository
{
    /// <summary>
    /// Describes a mechanism for Finding Accounts
    /// </summary>
    public interface IAccountFinder
    {
        void Initalise(ISetOfConfigrationSettings options);

        IAccount Find(IAccountCredentials credentials);
        IAccount Find(string emailAddress);
    }
}