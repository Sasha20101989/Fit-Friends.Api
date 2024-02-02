using Dapper;
using FitFriends.ServiceLibrary.Configurations;
using FitFriends.ServiceLibrary.DataAccess.Contracts;
using FitFriends.ServiceLibrary.Enums;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;

namespace FitFriends.ServiceLibrary.DataAccess
{
    /// <summary>
    /// Реализация доступа к данным SQL Server.
    /// </summary>
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly AppConfig _configuration;

        public SqlDataAccess(IOptions<AppConfig> configuration)
        {
            _configuration = configuration.Value;
        }

        /// <inheritdoc />
        public async Task SaveData(
            Enum storedProcedure,
            object? parameters = null,
            ConnectionId connectionId = ConnectionId.Default)
        {
            using IDbConnection connection = GetConnection(connectionId);

            await connection.ExecuteAsync(
                StoredProcedures.Map[storedProcedure.GetType()].Invoke(storedProcedure),
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<T>> LoadData<T>(
            Enum storedProcedure,
            object? parameters = null,
            ConnectionId connectionId = ConnectionId.Default
        )
        {
            using IDbConnection connection = GetConnection(connectionId);

            return await connection.QueryAsync<T>(
                StoredProcedures.Map[storedProcedure.GetType()].Invoke(storedProcedure),
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        private SqlConnection GetConnection(ConnectionId connectionId)
        {
            string connectionString;

            switch (connectionId)
            {
                default:
                    connectionString = _configuration.ConnectionStrings.Default;
                    break;
            }

            return new SqlConnection(connectionString);
        }
    }
}
