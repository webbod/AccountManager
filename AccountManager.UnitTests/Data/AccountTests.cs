using AccountManager.Data.DTO;
using AccountManager.Data.Helpers;
using AccountManager.Domain.Models;
using Xunit;

namespace AccountManager.UnitTests.Data
{
    public class AccountTests
    {
        private const string STANDARD = "standard";
        private const string DIFFERENT = "different";
        private const string DIFFERENT_PASSWORD = "different password"; 
        private const string DIFFERENT_EMAIL = "different email";
        private const string DIFFERENT_PASSWORD_CASE = "different password case";
        private const string DIFFERENT_EMAIL_CASE = "different email case";

        private AccountCredentials GenerateCredentials(string caseName)
        {
            switch(caseName)
            {
                case DIFFERENT_EMAIL:
                    return new AccountCredentials { EmailAddress = "a@me.il", PlainTextPassword = "Pa55w0rd!" };
                case DIFFERENT_PASSWORD:
                    return new AccountCredentials { EmailAddress = "e@ma.il", PlainTextPassword = "pas5W0rd!" };
                case DIFFERENT_EMAIL_CASE:
                    return new AccountCredentials { EmailAddress = "E@Ma.il", PlainTextPassword = "Pa55w0rd!" };
                case DIFFERENT_PASSWORD_CASE:
                    return new AccountCredentials { EmailAddress = "e@ma.il", PlainTextPassword = "pA55W0rD!" };
                case DIFFERENT:
                    return new AccountCredentials { EmailAddress = "a@me.il", PlainTextPassword = "pas5W0rd!" };
                case STANDARD:
                default:
                    return new AccountCredentials { EmailAddress = "e@ma.il", PlainTextPassword = "Pa55w0rd!" };
            }
        }

        [Fact]
        public void CreatingAnAccountFromCredentialsHashesThePassword()
        {
            var credentials = GenerateCredentials(STANDARD);
            var account = new Account(credentials);

            Assert.Equal(credentials.EmailAddress, account.EmailAddress);
            Assert.NotEqual(credentials.PlainTextPassword, account.HashedPassword);

            var expected = HashingService.HashPassword(credentials);
            Assert.Equal(expected, account.HashedPassword);
        }

        [Theory]
        [InlineData(STANDARD, STANDARD, true)]
        [InlineData(STANDARD, DIFFERENT, false)]
        [InlineData(STANDARD, DIFFERENT_EMAIL, false)]
        [InlineData(STANDARD, DIFFERENT_PASSWORD, false)]
        public void AnAccountWillOnlyMatchCredentialsIfEmailAddressAndPasswordAreTheSame(string accountCase, string credentialsCase, bool shouldMatch)
        {
            var account = new Account(GenerateCredentials(accountCase));
            var credentials = GenerateCredentials(credentialsCase);

            Assert.Equal(shouldMatch, account.Equals(credentials));
        }

        [Theory]
        [InlineData(STANDARD, DIFFERENT_EMAIL_CASE, true)]
        [InlineData(STANDARD, DIFFERENT_PASSWORD_CASE, false)]
        public void EmailAddressIsNotCaseSensitive(string accountCase, string credentialsCase, bool shouldMatch)
        {
            var account = new Account(GenerateCredentials(accountCase));
            var credentials = GenerateCredentials(credentialsCase);

            Assert.Equal(shouldMatch, account.Equals(credentials));
        }
    }
}
