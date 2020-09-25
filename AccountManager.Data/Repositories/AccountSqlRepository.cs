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
    /// Acts as a facade to the Account sql data store
    /// </summary>
    public class AccountSqlRepository :  IAccountFinder, IAccountUpdater, IAccountDeleter, IAccountInserter
    {
        private string _ConnectionString;

        public AccountSqlRepository() { }

        public void Initalise(ISetOfConfigrationSettings options)
        {
            _ConnectionString = options.ConnectionString;
        }

        /// <exception cref="KeyNotFoundException"></exception>
        public Interfaces.Accounts.IAccount Find(string emailAddress)
        {
            var account = new Account_Find(emailAddress, _ConnectionString).ExecuteQuery();

            if (account == null || account == default(Account))
            {
                throw new KeyNotFoundException();
            }

            return account;
        }

        public Interfaces.Accounts.IAccount Find(IAccountCredentials credentials)
        {
            AccountCredentials.TryValidate(credentials);
            return Find(credentials.EmailAddress);
        }


        /// <exception cref="InvalidOperationException"></exception>
        public int Delete(string emailAddress, bool deleteIt = false)
        {
            if (!deleteIt)
            {
                throw new InvalidOperationException();
            }

            return new Account_Delete(emailAddress, _ConnectionString).ExecuteNonQuery();
        }

        /// <exception cref="KeyNotFoundException">If the account is not found</exception>
        public Interfaces.Accounts.IAccount Update(IAccountCredentials credentials)
        {
            AccountCredentials.TryValidate(credentials);
            return UpdateAccount(credentials);
        }

        public Interfaces.Accounts.IAccount Insert(IAccountCredentials credentials)
        {
            AccountCredentials.TryValidate(credentials);
            return InsertAccount(credentials);
        }

        private Interfaces.Accounts.IAccount CreateAccount(IAccountCredentials credentials)
        {
            var account = new Account(credentials);
            account.Id = new Account_Insert(account, _ConnectionString).ExecuteNonQuery();

            return account;
        }

        /// <exception cref="NotSupportedException">If the account already exists</exception>
        private Interfaces.Accounts.IAccount InsertAccount(IAccountCredentials credentials)
        {
            try
            {
                Find(credentials.EmailAddress);
                throw new NotSupportedException();
            }
            catch (KeyNotFoundException)
            {
                var account = new Account(credentials);
                account.Id = new Account_Insert(account, _ConnectionString).ExecuteNonQuery();

                return account;
            }
        }

        private Interfaces.Accounts.IAccount UpdateAccount(IAccountCredentials credentials)
        {
            var account = ((Account)Find(credentials.EmailAddress)).ApplyNewCredentials(credentials);
            account.Id = new Account_Update(account, _ConnectionString).ExecuteNonQuery();

            return account;
        }


    }
}