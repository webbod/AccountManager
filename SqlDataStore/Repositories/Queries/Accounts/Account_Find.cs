﻿using AccountManager.SqlDataStore.DTO;
using AccountManager.SqlDataStore.Repositories.Mappers;
using AccountManager.Interfaces.Attributes;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AccountManager.SqlDataStore.Repositories.Queries.Accounts
{
    /// <summary>
    /// Models a command to find an Account
    /// </summary>
    internal class Account_Find : AGenericQuery<Account>
    {
        [DatabaseDependency(
            name:"Accounts", 
            DatabaseDependency.Element.Column, 
            DatabaseDependency.MaintainenceRisk.ParameterDrift
        )]
        public Account_Find(string emailAddress, string connectionString = null) : base(connectionString)
        {
            _Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.VarChar) { Value = emailAddress });
        }

        [DatabaseDependency(
            name: "AccountFind", 
            DatabaseDependency.Element.StoredProcedure
        )]
        public override Account ExecuteQuery()
        {
            return ExecuteQuery("AccountFind")?.FirstOrDefault();
        }

        public override Account Mapper(IDataRecord reader)
        {
            return new AccountMapper().Map(reader);
        }
    }
}