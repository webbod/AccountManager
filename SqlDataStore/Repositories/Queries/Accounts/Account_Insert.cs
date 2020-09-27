using AccountManager.SqlDataStore.DTO;

namespace AccountManager.SqlDataStore.Repositories.Queries.Accounts
{
    /// <summary>
    /// Models a command to insert an Account
    /// </summary>
    internal class Account_Insert : Account_Update
    {
        public Account_Insert(Account account, string connectionString = null) : base(account, connectionString)
        {
        }
    }
}
