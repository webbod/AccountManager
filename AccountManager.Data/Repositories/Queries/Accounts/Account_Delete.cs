using AccountManager.Data.DTO;
using AccountManager.Interfaces.Accounts;
using AccountManager.Interfaces.Attributes;
using System.Data;
using System.Data.SqlClient;

namespace AccountManager.Data.Repositories.Queries.Accounts
{
    /// <summary>
    /// Models a command to delete an Account
    /// </summary>
    internal class Account_Delete : AGenericQuery<Account>
    {
        [DatabaseDependency(
            name: "Accounts",
            DatabaseDependency.Element.Column,
            DatabaseDependency.MaintainenceRisk.ParameterDrift
        )]
        public Account_Delete(string emailAddress, string connectionString = null) : base(connectionString)
        {
            _Parameters.Add(new SqlParameter("@EmailAddress", SqlDbType.NVarChar) { Value = emailAddress });
        }


        [DatabaseDependency(
            name: "AccountDelete",
            DatabaseDependency.Element.StoredProcedure
        )]
        public override int ExecuteNonQuery()
        {
            return ExecuteNonQuery("AccountDelete");
        }
    }
}
