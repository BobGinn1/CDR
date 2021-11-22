using Dapper;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Logging;
using System.Linq;

namespace DataLayer
{
    public class DataAccess
    {
        private IDbConnection connectionString;
        public IDbConnection GetCDRConnection(IConfiguration configuration)
        {
            return connectionString = new SqlConnection(configuration.GetValue<string>("ConnectionStrings:CDRDatabase"));
        }

        public CDRModel GetCDRByID(IConfiguration configuration, int cdrId)
        {
            connectionString = GetCDRConnection(configuration);
            var parameters = new DynamicParameters();
            parameters.Add("@CDRId", cdrId);
            return connectionString.Query<CDRModel>("[GetCDRByID]", parameters, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
        private DateTime ValidateAndProtectDates(IConfiguration configuration, DateTime start, DateTime end)
        {
            var maxDifference = configuration.GetValue<int>("DateDifference:MaxAllowedDifference");
            if (start.AddMonths(maxDifference) < end)
            {
                end = start.AddMonths(maxDifference);
            }
            if (start.AddMonths(-maxDifference) > end)
            {
                end = start.AddMonths(-maxDifference);
            }
            return end;
        }
        public List<CDRModel> GetCallCountAndDurationByDateForCallerId(IConfiguration configuration, DateTime start, DateTime end, string callerId, int? type = null)
        {
            end = ValidateAndProtectDates(configuration, start, end);
            connectionString = GetCDRConnection(configuration);
            var parameters = new DynamicParameters();
            parameters.Add("@Start", start);
            parameters.Add("@End", end);
            parameters.Add("@CallerId", callerId);
            parameters.Add("@TypeId", type);
            return connectionString.Query<CDRModel>("[GetCallCountAndDurationByDate]", parameters, commandType: CommandType.StoredProcedure).ToList();
        }

        public List<CDRModel> GetMostExpensiveCallCountByDateForCallerId(IConfiguration configuration, DateTime start, DateTime end, string callerId, int numberToReturn, int? type = null)
        {
            connectionString = GetCDRConnection(configuration);
            end = ValidateAndProtectDates(configuration, start, end);
            var parameters = new DynamicParameters();
            parameters.Add("@Start", start);
            parameters.Add("@End", end);
            parameters.Add("@CallerId", callerId);
            parameters.Add("@ReturnCount", numberToReturn);
            parameters.Add("@TypeId", type);
            return connectionString.Query<CDRModel>("[GetMostExpensiveCallsForCallerID]", parameters, commandType: CommandType.StoredProcedure).ToList();
        }

        public void InsertCDRRecords(IConfiguration configuration, List<CDRModel> cdrList)
        {
            connectionString = GetCDRConnection(configuration);
            Logger logger = new Logger();
            var parameters = new DynamicParameters();
            foreach (var cdr in cdrList)
            {
                try
                {
                    
                    parameters.Add("@CallerId", cdr.CallerId);
                    parameters.Add("@Recipient", cdr.Recipient);
                    parameters.Add("@CallDate", cdr.CallDate);
                    parameters.Add("@EndTime", cdr.EndTime);
                    parameters.Add("@Duration", cdr.Duration);
                    parameters.Add("@Cost", cdr.Cost);
                    parameters.Add("@Reference", cdr.Reference);
                    parameters.Add("@Currency", cdr.Currency);
                    parameters.Add("@TypeId", cdr.TypeId);
                    connectionString.ExecuteScalar("[InsertCDRRecords]", parameters, commandType: CommandType.StoredProcedure);
                }
                catch (Exception ex)
                {
                    logger.WriteToLog(configuration, "Error Message: " + ex.Message);
                    logger.WriteToLog(configuration, "Failing insertion: " + parameters.ToString());
                }
            }
           
        }
    }
}
