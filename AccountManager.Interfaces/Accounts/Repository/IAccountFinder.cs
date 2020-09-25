using AccountManager.Interfaces.DataStore;

namespace AccountManager.Interfaces.Accounts.Repository
{
    /// <summary>
    /// Describes a mechanism for Finding Accounts
    /// </summary>
    public interface IAccountFinder : IFinder<string, IAccount>
    {
        IAccount Find(IAccountCredentials credentials);   
    }
}