using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.Accounts.Repository
{
    /// <summary>
    /// Describes a mechanism for Deleting Accounts
    /// </summary>
    public interface IAccountDeleter
    {
        void Initalise(ISetOfConfigrationSettings options);
        int Delete(string emailAddress, bool deleteIt = false);
    }
}