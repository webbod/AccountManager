using AccountManager.Domain.Models;
using System;
using Xunit;

namespace AccountManager.UnitTests.Domain
{
    public class AccountCredentialsTests
    {
        private const string VALID_PASSWORD = "Pa55w0rd!";
        private const string VALID_EMAILADDRESS = "email@mail.com";

        private bool TestTryValidate(AccountCredentials credentials)
        {
            try
            {
                return AccountCredentials.TryValidate(credentials);
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }

        [Theory]
        [InlineData("", null, false)]
        [InlineData(VALID_EMAILADDRESS, "", false)]
        [InlineData("", VALID_PASSWORD, false)]
        [InlineData(VALID_EMAILADDRESS, VALID_PASSWORD, true)]
        public void CredentialsAreValidIfNothingIsNullOrEmpty(string emailAddress, string plainTextPassword, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = emailAddress, PlainTextPassword = plainTextPassword };
            Assert.Equal(shouldBeValid, TestTryValidate(credentials));
        }

        [Theory]
        [InlineData("e@l", false)]
        [InlineData("email@mail.commmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmmm", false)]
        [InlineData(VALID_EMAILADDRESS, true)]
        public void AnEmailAddressShouldBeTheCorrectLength(string emailAddress, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = emailAddress, PlainTextPassword = VALID_PASSWORD };
            Assert.Equal(TestTryValidate(credentials), shouldBeValid);
        }

        [Theory]
        [InlineData("Pa55!", false)]
        [InlineData("Pa55!Pa55!Pa55!Pa55!Pa55!Pa55!Pa55!", false)]
        [InlineData(VALID_PASSWORD, true)]
        public void APasswordShouldBeTheCorrectLength(string password, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = VALID_EMAILADDRESS, PlainTextPassword = password };
            Assert.Equal(TestTryValidate(credentials), shouldBeValid);
        }

        [Theory]
        [InlineData("Password!", false)]    // no number
        [InlineData("pa55w0rd", false)]     // no capital letter
        [InlineData("PA55W0RD!", false)]    // no lowercase letter
        [InlineData("Pa55w0rd", false)]     // no symbol
        [InlineData(VALID_PASSWORD, true)]
        public void APasswordShouldBeInTheCorrectFormat(string password, bool shouldBeValid)
        {
            var credentials = new AccountCredentials { EmailAddress = VALID_EMAILADDRESS, PlainTextPassword = password };
            Assert.Equal(TestTryValidate(credentials), shouldBeValid);
        }
    }
}
