using AccountManager.Data.DTO;
using AccountManager.Data.Repositories.Mappers;
using AccountManager.Interfaces.Attributes;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AccountManager.Data.Repositories.Queries.Accounts
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
            name:"Accounts", 
            DatabaseDependency.Element.Table, 
            DatabaseDependency.MaintainenceRisk.RawSQLStatement
        )]
        public override Account ExecuteQuery()
        {
            return ExecuteQuery("Account_Find")?.FirstOrDefault();
        }

        public override Account Mapper(IDataRecord reader)
        {
            return new AccountMapper().Map(reader);
        }
    }
}