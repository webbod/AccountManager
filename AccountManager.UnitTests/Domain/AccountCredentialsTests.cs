using AccountManager.Domain.Models;
using System;
using Xunit;

namespace AccountManager.UnitTests.Domain
{
    public class AccountCredentialsTests
    {
        [Theory]
        [InlineData("", null, true)]
        [InlineData("e@ma.il", "", true)]
        [InlineData("", "password", true)]
        [InlineData("e@ma.il", "password", false)]
        public void CredentialsAreValidIfNothingIsNullOrEmpty(string emailAddress, string plainTextPassword, bool shouldThrowException)
        {
            var credentials = new AccountCredentials { EmailAddress = emailAddress, PlainTextPassword = plainTextPassword };

            try
            {
                var actual = AccountCredentials.TryValidate(credentials);
                Assert.True(actual);
            }
            catch(ArgumentNullException)
            {
                Assert.True(shouldThrowException);
            }   
        }

        [Theory]
        [InlineData("e@l", false)]
        [InlineData("email@mail.commmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm", false)]
        [InlineData("email@mail.com", true)]
        public void AnEmailAddressShouldBeTheCorrectLength(string emailAddress, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = emailAddress, PlainTextPassword = "P@55w0rd!" };
            Assert.Equal(shouldBeValid, credentials.EmailAddressIsValid);
        }

        [Theory]
        [InlineData("Pa55!", false)]
        [InlineData("Pa55!Pa55!Pa55!Pa55!Pa55!Pa55!Pa55!", false)]
        [InlineData("Pa55w0rd!", true)]
        public void APasswordShouldBeTheCorrectLength(string password, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = "email@mail.com", PlainTextPassword = password };
            Assert.Equal(shouldBeValid, credentials.PlainTextPasswordIsValid);
        }

        [Theory]
        [InlineData("Password!", false)]    // no number
        [InlineData("pa55w0rd", false)]     // no capital letter
        [InlineData("PA55W0RD!", false)]    // no lowercase letter
        [InlineData("Pa55w0rd", false)]     // no symbol
        [InlineData("Pa55w0rd!", true)]
        public void APasswordShouldBeInTheCorrectFormat(string password, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = "email@mail.com", PlainTextPassword = password };
            Assert.Equal(shouldBeValid, credentials.PlainTextPasswordIsValid);
        }
    }
}
