using AccountManager.SqlDataStore.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccountManager.SqlDataStore.Repositories.Queries
{
    /// <summary>
    /// Mediates the interface between the business logic and the sql dataStore
    /// </summary>
    /// <typeparam name="TReturnType"></typeparam>
    public abstract class AGenericQuery<TReturnType> where TReturnType : class
    {
        protected List<SqlParameter> _Parameters;
        protected string _ConnectionString;

        public AGenericQuery()
        {
        }

        public AGenericQuery(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
            _Parameters = new List<SqlParameter>();
        }

        public virtual TReturnType ExecuteQuery()
        {
            // call ExecuteQuery(commandText) from child class
            throw new NotImplementedException();
        }

        public virtual int ExecuteNonQuery()
        {
            // call ExecuteNonQuery(commandText) from child class
            throw new NotImplementedException();
        }

        public virtual TReturnType Mapper(IDataRecord reader)
        {
            // implement the mapper in an external class so it can be tested
            throw new NotImplementedException();
        }

        /// <summary>
        /// Executes the query
        /// </summary>
        /// <param name="commandText">command to run</param>
        /// <returns>list of mapped records</returns>
        protected List<TReturnType> ExecuteQuery(string commandText)
        {
            using (SqlCommand _Command = QueryService.ConfigureStoredProcedureCommand(commandText, _Parameters, _ConnectionString))
            {
                return QueryService.MapResults(_Command, Mapper);
            }
        }

        /// <summary>
        /// Executes the command and returns a return code (usually the inserted/deleted item Id)
        /// </summary>
        /// <param name="commandText">command to run</param>
        /// <returns>output of the command</returns>
        protected int ExecuteNonQuery(string commandText)
        {
            var returnParam = new SqlParameter("@ReturnValue", SqlDbType.Int) { Direction = ParameterDirection.ReturnValue };
            _Parameters.Add(returnParam);

            using (SqlCommand _Command = QueryService.ConfigureStoredProcedureCommand(commandText, _Parameters, _ConnectionString))
            {
                _Command.ExecuteNonQuery();
                QueryService.DisposeOfConnection(_Command);

                return (int)returnParam.Value;
            }
        }
    }
}
