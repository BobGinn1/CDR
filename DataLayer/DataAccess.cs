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
        public IDbConnection GetCDRConnection(IConfiguration configuration)
        {
            return connectionString = new SqlConnection(configuration.GetConnectionString("CDRDatabase"));
        }

        public CDRModel GetCDRByID(IConfiguration configuration, int cdrId)
        {
            connectionString = GetCDRConnection(configuration);
            var parameters = new DynamicParameters();
            parameters.Add("@CDRId", cdrId);
            return (CDRModel)connectionString.ExecuteScalar("[GetCDRByID]", parameters, commandType: CommandType.StoredProcedure);
        }
        public CDRModel GetCallCountAndDurationByDateForCallerId(IConfiguration configuration, DateTime start, DateTime end, string callerId, CallTypeModel type = null)
        {
            connectionString = GetCDRConnection(configuration);
            var parameters = new DynamicParameters();
            parameters.Add("@Start", start);
            parameters.Add("@End", end);
            parameters.Add("@CallerId", callerId);
            parameters.Add("@TypeId", type);
            return (CDRModel)connectionString.ExecuteScalar("[GetCallCountAndDurationByDate]", parameters, commandType: CommandType.StoredProcedure);
        }

        public CDRModel GetMostExpensiveCallCountByDateForCallerId(IConfiguration configuration, DateTime start, DateTime end, string callerId, int numberToReturn, CallTypeModel type = null)
        {
            connectionString = GetCDRConnection(configuration);
            var parameters = new DynamicParameters();
            parameters.Add("@Start", start);
            parameters.Add("@End", end);
            parameters.Add("@CallerId", callerId);
            parameters.Add("@ReturnCount", numberToReturn);
            parameters.Add("@TypeId", type);
            return (CDRModel)connectionString.ExecuteScalar("[GetMostExpensiveCallsForCallerID]", parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
