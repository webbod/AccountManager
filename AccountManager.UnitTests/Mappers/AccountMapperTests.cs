using AccountManager.SqlDataStore.Repositories.Mappers;
using AccountManager.UnitTests.Mocks.DataRecords;
using Xunit;

namespace AccountManager.UnitTests.Data
{
    public class AccountMapperTests
    {
        [Fact]
        public void TheAccountMapperWorks()
        {
            var id = 100;
            var emailAddress = "e@ma.il";
            var hashedPassword = "783jdksl;'[pxnhgy6733;!shuSA,dGH3";

            var mockAccountRecord = new MockAccountRecord(id, emailAddress, hashedPassword);
            var mappedAccount = new AccountMapper().Map(mockAccountRecord);

            Assert.Equal(id, mappedAccount.Id);
            Assert.Equal(emailAddress, mappedAccount.EmailAddress);
            Assert.Equal(hashedPassword, mappedAccount.HashedPassword);

        }
    }
}
