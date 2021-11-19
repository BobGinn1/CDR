using DataLayer;
using DataLayer.Models;
using Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq;

namespace FileProcessor
{
    public class CSVProcessor
    {
        public void LoadCDRData(IConfiguration configuration, string filePath)
        {
            DataAccess dataAccess = new DataAccess();
            List<CDRModel> cdrList = new List<CDRModel>();
            Logger logger = new Logger();

            var files = System.IO.Directory.GetFiles(filePath, "*.csv", SearchOption.AllDirectories);
            var completeDirectory = configuration.GetValue<string>("Directories:Done");

            foreach (var file in files)
            {
                var lines = System.IO.File.ReadAllLines(file).Skip(1);

                foreach (var line in lines)
                {
                    CDRModel cdr = new CDRModel();
                    var lineDetails = line.Split(",");
                    try
                    {
                        cdr.CallerId = lineDetails[0];
                        cdr.Recipient = lineDetails[1];
                        cdr.CallDate = DateTime.Parse(lineDetails[2]);
                        cdr.EndTime = TimeSpan.Parse(lineDetails[3]);
                        cdr.Duration = Int32.Parse(lineDetails[4]);
                        cdr.Cost = Decimal.Parse(lineDetails[5]);
                        cdr.Reference = lineDetails[6];
                        cdr.Currency = lineDetails[7];
                        cdr.TypeId = Int32.Parse(lineDetails[8]);
                    }
                    catch(Exception ex)
                    {
                        logger.WriteToLog(configuration, "Error Message: " + ex.Message);
                        logger.WriteToLog(configuration, "Failing line: " + lineDetails);
                    }

                    cdrList.Add(cdr);
                }

                if (File.Exists(completeDirectory + Path.GetFileName(file)))
                {
                    File.Delete(completeDirectory + Path.GetFileName(file));
                }

                File.Move(file, completeDirectory + Path.GetFileName(file));
            }
            dataAccess.InsertCDRRecords(configuration, cdrList);

        }
    }
}
