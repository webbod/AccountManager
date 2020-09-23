using AccountManager.Data.DTO;
using AccountManager.Data.Repositories.Queries.Accounts;
using AccountManager.Domain.Models;
using AccountManager.Interfaces.DataStore;
using AccountManager.Interfaces.Accounts;
using AccountManager.Interfaces.Accounts.Repository;
using System.Collections.Generic;
using System.Data.Common;
using System;

namespace AccountManager.Data.Repositories
{
    /// <summary>
    /// Acts as a facade to the Account data store
    /// </summary>
    public class AccountRepository : IAccountFinder, IAccountUpdater, IAccountDeleter
    {
        private string _ConnectionString;

        public AccountRepository() { }

        public void Initalise(ISetOfConfigrationSettings options)
        {
            _ConnectionString = options.ConnectionString;
        }

        /// <exception cref="KeyNotFoundException"></exception>
        public IAccount Find(string emailAddress)
        {
            var account = new Account_Find(emailAddress, _ConnectionString).ExecuteQuery();

            if (account == null || account == default(Account))
            {
                throw new KeyNotFoundException();
            }

            return account;
        }

        public IAccount Find(IAccountCredentials credentials)
        {
            AccountCredentials.TryValidate(credentials);
            return Find(credentials.EmailAddress);
        }

        public int Delete(string emailAddress, bool deleteIt = false)
        {
            if (!deleteIt)
            {
                throw new InvalidOperationException();
            }

            return new Account_Delete(emailAddress, _ConnectionString).ExecuteNonQuery();
        }

        public IAccount Update(IAccountCredentials credentials)
        {
            AccountCredentials.TryValidate(credentials);
            
            try
            {
                return UpdateAccount(credentials);
            }
            catch(KeyNotFoundException)
            {
                return CreateAccount(credentials);
            }
        }

        private IAccount CreateAccount(IAccountCredentials credentials)
        {
            var account = new Account(credentials);
            account.Id = new Account_Insert(account, _ConnectionString).ExecuteNonQuery();

            return account;
        }

        private IAccount UpdateAccount(IAccountCredentials credentials)
        {
            var account = ((Account)Find(credentials.EmailAddress)).ApplyNewCredentials(credentials);
            account.Id = new Account_Update(account, _ConnectionString).ExecuteNonQuery();

            return account;
        }
    }
}