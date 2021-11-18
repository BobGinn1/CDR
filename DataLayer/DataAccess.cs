using Dapper;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DataLayer
{
    public class DataAccess
    {
        private IDbConnection connectionString;
        public IDbConnection GetCDRConnection(IConfiguration configuration, string connection)
        {
            return connectionString = new SqlConnection(configuration.GetConnectionString("CDRDatabase"));
        }

        public CDRModel GetCDRByID(IConfiguration configuration, int cdrId)
        {
            connectionString = GetCDRConnection(configuration, sourceName);
            var parameters = new DynamicParameters();
            parameters.Add("@datasource_id", dataSourceID);
            return (int)connectionString.ExecuteScalar("[map].[GetPropertyCount]", parameters, commandType: CommandType.StoredProcedure);
        }
        public CDRModel GetCallCountAndDurationByDate(IConfiguration configuration, DateTime start, DateTime end, string callerId CallType type = null)
        {
            connectionString = GetCDRConnection(configuration, sourceName);
            var parameters = new DynamicParameters();
            parameters.Add("@datasource_id", dataSourceID);
            return (int)connectionString.ExecuteScalar("[map].[GetPropertyCount]", parameters, commandType: CommandType.StoredProcedure);
        }

        public CDRModel GetMostExpensiveCallCountByCallerId(IConfiguration configuration, DateTime start, DateTime end, string callerId, CallType type = null)
        {
            connectionString = GetCDRConnection(configuration, sourceName);
            var parameters = new DynamicParameters();
            parameters.Add("@datasource_id", dataSourceID);
            return (int)connectionString.ExecuteScalar("[map].[GetPropertyCount]", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
