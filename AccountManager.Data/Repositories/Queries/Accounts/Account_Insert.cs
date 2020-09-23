using AccountManager.Data.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace AccountManager.Data.Repositories.Queries.Accounts
{
    /// <summary>
    /// Models a command to insert an Account
    /// </summary>
    internal class Account_Insert : Account_Update
    {
        public Account_Insert(Account account, string connectionString = null) : base(account, connectionString)
        {
        }
    }
}
