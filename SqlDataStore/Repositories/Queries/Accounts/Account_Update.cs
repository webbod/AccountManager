using AccountManager.SqlDataStore.DTO;
using AccountManager.Interfaces.Attributes;
using System.Data;
using System.Data.SqlClient;

namespace AccountManager.SqlDataStore.Repositories.Queries.Accounts
{
    /// <summary>
    /// Models a command to update an Account
    /// </summary>
    internal class Account_Update : AGenericQuery<Account>
    {
        [DatabaseDependency(
            name: "Accounts",
            DatabaseDependency.Element.Column,
            DatabaseDependency.MaintainenceRisk.ParameterDrift
        )]
        public Account_Update(Account account, string connectionString = null) : base(connectionString)
        {
            _Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = account.Id });
            _Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar) { Value = account.EmailAddress });
            _Parameters.Add(new SqlParameter("@HashedPassword", SqlDbType.NVarChar) { Value = account.HashedPassword });
        }


        [DatabaseDependency(
            name: "AccountUpdate",
            DatabaseDependency.Element.StoredProcedure
        )]
        public override int ExecuteNonQuery()
        {
            return ExecuteNonQuery("AccountUpdate");
        }
    }
}