using AccountManager.Domain.Services;
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
    }
}
