using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AccountManager.Data.Services
{
    /// <summary>
    /// Pulls all of the SqlCommand, SqlConnection and SqlDataReader references into one place 
    /// There is no need for any other class to have a direct dependency on System.Data.SqlClient
    /// </summary>
    internal static class QueryService
    {
        public static string ConfigureConnectionString(string connectionString = null)
        {
            if(string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException();
            }

            return connectionString;
        }

        public static SqlConnection CreateConnection(string connectionString = null)
        {
            return new SqlConnection(ConfigureConnectionString(connectionString));
        }

        public static SqlConnection EstablishOpenConnection(string connectionString = null)
        {
            var _Connection = CreateConnection(connectionString);
            _Connection.Open();

            return _Connection;
        }

        public static SqlCommand CreateCommand(string commandText, CommandType commandType, string connectionString = null)
        {
            return new SqlCommand
            {
                Connection = EstablishOpenConnection(connectionString),
                CommandType = commandType,
                CommandText = commandText
            };
        }

        public static SqlCommand CommandWithParameters(SqlCommand command, List<SqlParameter> parameters)
        {
            command.Parameters.AddRange(parameters.ToArray());
            return command;
        }

        public static SqlCommand ConfigureStoredProcedureCommand(string commandText, List<SqlParameter> parameters = null, string connectionString = null)
        {
            return CommandWithParameters(
                CreateCommand(commandText, CommandType.StoredProcedure, connectionString),
                parameters
            );
        }

        public static SqlDataReader CreateReader(SqlCommand command, CommandBehavior commandBehavior = CommandBehavior.CloseConnection)
        {
            return command.ExecuteReader(commandBehavior);
        }

        /// <summary>
        /// Maps the field in a DataReader into the specified tyoe
        /// </summary>
        /// <typeparam name="TOutput">the class to populate</typeparam>
        /// <param name="command">a configured SqlCommand object</param>
        /// <param name="mapper">a function to map fields in a record to properties in the class</param>
        /// <returns>a list of populated TObjects</returns>
        public static List<TOutput> MapResults<TOutput>(SqlCommand command, Func<IDataRecord, TOutput> mapper) 
            where TOutput : class
        {
            var _Output = new List<TOutput>();

            using (var _Reader = CreateReader(command))
            {
                while (_Reader.Read())
                {
                    _Output.Add(mapper(_Reader));
                }
            }

            DisposeOfConnection(command);
            return _Output;
        }

        public static void DisposeOfConnection(SqlCommand command)
        {
            command.Connection.Dispose();
        }
    }
}
