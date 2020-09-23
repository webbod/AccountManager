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
    }
}
