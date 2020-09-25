using AccountManager.Data;
using AccountManager.Data.DTO;
using AccountManager.Data.Helpers;
using AccountManager.Data.Repositories;
using AccountManager.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace AccountManager.UnitTests.Data
{
    public class  AccountRepositioryTests
    {
        private AccountSqlRepository _AccountRepository;

        public AccountRepositioryTests()
        {
            var config = new SqlDataStoreConfigurationSettings
            {
                ConnectionString = "Server=localhost\\sqlexpress;Database=AccountManagerDemo;User Id=test;Password=test;"
            };

            _AccountRepository = new AccountSqlRepository();
            _AccountRepository.Initalise(config);
        }

        private AccountCredentials GenerateCredentials()
        {
            return new AccountCredentials
            {
                EmailAddress = $"{Guid.NewGuid().ToString()}@test.com",
                PlainTextPassword = $"Aa1!{Guid.NewGuid().ToString()}".Substring(0, 32)
            };
        }

        private void CleanUp(string emailAddress)
        {
            _AccountRepository.Delete(emailAddress, true);
        }

        [Fact]
        public void ANewAccountCanBeInsertedIntoTheDataStore()
        {
            var credentials = GenerateCredentials();
            var account = _AccountRepository.Insert(credentials);

            var unexpected = 0;
            Assert.NotEqual(unexpected, account.Id);

            CleanUp(account.EmailAddress);
        }

        [Fact]
        public void AnAccountCanBeFoundByEmailAddress()
        {
            var credentials = GenerateCredentials();
            var account = _AccountRepository.Insert(credentials);

            var otherAccount = _AccountRepository.Find(account.EmailAddress);

            Assert.Equal(account.Id, otherAccount.Id);

            CleanUp(account.EmailAddress);
        }

        [Fact]
        public void ADeletedAccountCanNoLongerBeFound()
        {
            var credentials = GenerateCredentials();
            var account = _AccountRepository.Insert(credentials);

            _AccountRepository.Delete(account.EmailAddress, true);

            Assert.Throws<KeyNotFoundException>(() => _AccountRepository.Find(account.EmailAddress));
        }

        [Fact]
        public void UpdatingAnExistingAccountCanOnlyChangeThePassword()
        {
            var initialCredentials = GenerateCredentials();
            var updatedCredentials = new AccountCredentials { 
                EmailAddress = initialCredentials.EmailAddress,
                PlainTextPassword = "Zz0)dgh3628..AA" 
            };

            var account = _AccountRepository.Insert(initialCredentials);

            var initialAccountId = account.Id;
            var initialHashedPassword = ((Account)account).HashedPassword;

            Assert.Equal(initialCredentials.EmailAddress, account.EmailAddress);

            account = _AccountRepository.Update(updatedCredentials);

            Assert.Equal(initialCredentials.EmailAddress, account.EmailAddress);
            Assert.Equal(initialAccountId, account.Id);
            Assert.NotEqual(initialHashedPassword, ((Account)account).HashedPassword);

            CleanUp(account.EmailAddress);
        }

        [Fact]
        public void YouCannotUpdateAnAccountThatDoesNotExist()
        {
            var credentials = GenerateCredentials();
            Assert.Throws<KeyNotFoundException>(() => _AccountRepository.Update(credentials));
        }

        [Fact]
        public void YouCannotInsertTheSameAccountMoreThanOnce()
        {
            var credentials = GenerateCredentials();

            var account = _AccountRepository.Insert(credentials);

            Assert.Throws<NotSupportedException>(() => _AccountRepository.Insert(credentials));

            CleanUp(account.EmailAddress);
        }

    }
}
