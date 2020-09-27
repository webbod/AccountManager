using AccountManager.Domain.Services;
using AccountManager.Interfaces.Accounts;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountManager.Domain.Extensions
{
    public static class IAccountCredentialsExtensions
    {
        /// <summary>
        /// Confirms that the email address and password have been set
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns>validated credentials</returns>
        /// <exception cref="ArgumentNullException">if null or empty</exception>
        public static bool TryValidate(this IAccountCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials?.EmailAddress) ||
                string.IsNullOrEmpty(credentials?.PlainTextPassword))
                throw new ArgumentNullException();

            if (!credentials.EmailAddressIsValid ||
                !credentials.PlainTextPasswordIsValid)
                throw new ArgumentOutOfRangeException();

            return true;
        }

        public static string HashPassword(this IAccountCredentials credentials)
        {
            return HashingService.HashPassword(credentials);
        }
    }
}
