namespace AccountManager.Interfaces.Accounts
{
    /// <summary>
    /// Models a set of insecure account credentials
    /// </summary>
    public interface IAccountCredentials
    {
        string EmailAddress { get; set; }
        string PlainTextPassword { get; set; }
    }
}