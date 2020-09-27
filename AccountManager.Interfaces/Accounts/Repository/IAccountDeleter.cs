using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.Accounts.Repository
{
    /// <summary>
    /// Describes a mechanism for Deleting Accounts
    /// </summary>
    public interface IAccountDeleter : IDeleter<string>
    {
    }
}
