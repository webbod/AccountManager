using AccountManager.Interfaces.Accounts;
using System;

namespace AccountManager.Domain.Models
{
    public class AccountCredentials : IAccountCredentials
    {
        public virtual string EmailAddress { get; set; }

        public virtual string PlainTextPassword { get; set; }

        /// <summary>
        /// Confirms that the email address and password have been set
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>validated credentials</returns>
        /// <exception cref="ArgumentNullException">if null or empty</exception>
        public static bool TryValidate(IAccountCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials?.EmailAddress) || 
                string.IsNullOrEmpty(credentials?.PlainTextPassword))
                throw new ArgumentNullException();

            return true;
        }
    }
}
