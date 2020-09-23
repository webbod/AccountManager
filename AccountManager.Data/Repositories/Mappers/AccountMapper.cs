using AccountManager.Data.DTO;
using AccountManager.Interfaces.DataStore;
using System.Data;

namespace AccountManager.Data.Repositories.Mappers
{
    /// <summary>
    /// Maps fields in a DataRecord in to an Account
    /// </summary>
    internal class AccountMapper : IDataMapper<Account>
    {
        public Account Map(IDataRecord reader)
        {
            return new Account
            {
                Id = (int)reader["Id"],
                EmailAddress = (string)reader["EmailAddress"],
                HashedPassword = (string)reader["HashedPassword"]
            };
        }
    }
}
