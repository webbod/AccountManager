namespace AccountManager.Interfaces.Accounts
{
    /// <summary>
    /// Models an Account
    /// </summary>
    public interface IAccount
    {
        int Id { get; }
        string EmailAddress { get; }
        
        bool Equals(IAccountCredentials credentials);
    }
}