using DataLayer;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
    public class DataTests
    {
        private IConfiguration _config;

        public DataTests()
        {
           
        }

        [SetUp]
        public void Setup()
        {
            _config = InitConfiguration();
        }
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }

        [Test]
        public void GetSingleCDR_DataCheck()
        {
            
            var clientId = _config.GetValue<string>("ConnectionStrings:CDRDatabase");
            DataAccess da = new DataAccess();
            var cdr = MockSingleCDR();
            var result = da.GetCDRByID(_config, 5) as CDRModel;
            var expectedJson = JsonConvert.SerializeObject(cdr);
            var actualJson = JsonConvert.SerializeObject(result);

            Assert.AreEqual(expectedJson, actualJson);
        }
        public static CDRModel MockSingleCDR()
        {
            var cdr = new CDRModel();
            cdr.Id = 5;
            cdr.CallerId = "442036000000";
            cdr.Recipient = "448007000000";
            cdr.CallDate = DateTime.Parse("2016-08-16 00:00:00.000");
            cdr.EndTime = TimeSpan.Parse("14:33:32.0000000");
            cdr.Duration = 64;
            cdr.Cost = Decimal.Parse("0.000");
            cdr.Reference = "CE9BABA57E4CA258BCF66B8FC2E206965";
            cdr.Currency = "GBP";
            cdr.TypeId = 2;
            return cdr;
        }
    }
}