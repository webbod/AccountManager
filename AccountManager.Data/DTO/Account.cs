using AccountManager.Data.Helpers;
using AccountManager.Interfaces.Accounts;

namespace AccountManager.Data.DTO
{
    /// <summary>
    /// Models an Account record
    /// </summary>
    public class Account : IAccount
    {
        // this is a write-once-read-many property
        private int _Id;
        public int Id
        {
            get => _Id;
            internal set
            {
                if (_Id <= 0) { _Id = value; }
            }
        }

        // this is a worm property
        private string _EmailAddress;
        public string EmailAddress
        {
            get => _EmailAddress;
            internal set
            {
                if(string.IsNullOrEmpty(_EmailAddress)) { _EmailAddress = value; }
            }
        }

        // Nothing needs to know about this outside of the data layer
        private string _HashedPassword;
        internal string HashedPassword {
            get => _HashedPassword;
            set
            {
                if(!string.IsNullOrEmpty(value)) { _HashedPassword = value; }
            }
        }

        internal Account() { }

        /// <summary>
        /// Creates a new Account from the credentials
        /// </summary>
        /// <param name="credentials"></param>
        internal Account(IAccountCredentials credentials)
        {
            EmailAddress = credentials.EmailAddress;
            UpdateHashedPassword(credentials);
        }

        private void UpdateHashedPassword(IAccountCredentials credentials)
        {
            HashedPassword = HashingService.HashPassword(credentials);
        }

        internal Account ApplyNewCredentials(IAccountCredentials credentials)
        {
            UpdateHashedPassword(credentials);
            return this;
        }

        /// <summary>
        /// Tests if the credentials provided match those of the Account
        /// </summary>
        public bool Equals(IAccountCredentials credentials)
        {
            var hashedPassword = HashingService.HashPassword(credentials);

            return
                EmailAddress.ToLower() == credentials.EmailAddress.ToLower() &&
                HashedPassword == hashedPassword;
        }
    }
}
