using AccountManager.Interfaces.Accounts;
using System;
using System.Text.RegularExpressions;

namespace AccountManager.Domain.Models
{
    public class AccountCredentials : IAccountCredentials
    {
        #region Validation Criteria

        public const int PlainTextPasswordMinLength = 8;
        public const int PlainTextPasswordMaxLength = 32;
        public const string PlainTextPasswordPattern = @"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[^\da-zA-Z]).*$";
        public const string PlainTestPatternErrorMessage = "Password needs letters, numbers and symbols";

        public const int EmailAddressMinLength = 5;
        public const int EmailAddressMaxLength = 200;

        #endregion

        public virtual string EmailAddress { get; set; }
        public bool EmailAddressIsValid
        {
            get
            {
                return EmailAddress?.Length >= EmailAddressMinLength && EmailAddressMaxLength >= EmailAddress.Length;
            }
        }

        public virtual string PlainTextPassword { get; set; }
        public bool PlainTextPasswordIsValid
        {
            get
            {
                var regX = new Regex(PlainTextPasswordPattern);

                return
                    PlainTextPassword?.Length >= PlainTextPasswordMinLength &&
                    PlainTextPasswordMaxLength >= PlainTextPassword.Length &&
                    regX.IsMatch(PlainTextPassword);
            }
        }

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

            if (!credentials.EmailAddressIsValid ||
                !credentials.PlainTextPasswordIsValid)
                throw new ArgumentOutOfRangeException();

            return true;
        }
    }
}
