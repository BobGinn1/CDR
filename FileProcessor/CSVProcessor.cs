using DataLayer;
using DataLayer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace FileProcessor
{
    public class CSVProcessor
    {
        public void LoadCDRData(IConfiguration configuration)
        {
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdrList = new List<CDRModel>();

            var fileDirectory = configuration.GetValue<string>("Directories:File");
            var files = System.IO.Directory.GetFiles(fileDirectory, "*.csv", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                var lines = System.IO.File.ReadAllLines(file).Skip(1);

                foreach (var line in lines)
                {
                    CDRModel cdr = new CDRModel();
                    var lineDetails = line.Split(",");

                    cdr.CallerId = lineDetails[0];
                    cdr.Recipient = lineDetails[1];
                    cdr.CallDate = DateTime.Parse(lineDetails[2]);
                    cdr.EndTime = TimeSpan.Parse(lineDetails[3]);
                    cdr.Duration = TimeSpan.Parse(lineDetails[4]);
                    cdr.Cost = Decimal.Parse(lineDetails[5]);
                    cdr.Reference = lineDetails[6];
                    cdr.Currency = lineDetails[7];
                    cdr.TypeId = Int32.Parse(lineDetails[8]);

                    cdrList.Add(cdr);
                }
            }
            dataAccess.InsertCDRRecords(configuration, cdrList);
        }
    }
}
