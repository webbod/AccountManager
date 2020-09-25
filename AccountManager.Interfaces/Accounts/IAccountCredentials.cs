namespace AccountManager.Interfaces.Accounts
{
    /// <summary>
    /// Describes a set of insecure account credentials
    /// </summary>
    public interface IAccountCredentials
    {
        string EmailAddress { get; set; }
        bool EmailAddressIsValid { get; }

        string PlainTextPassword { get; set; }
        bool PlainTextPasswordIsValid { get; }
    }
}