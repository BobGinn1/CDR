using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Logging
{
    public class Logger
    {
        public void WriteToLog(IConfiguration config, string message)
        {
            var path = config.GetValue<string>("Directories:Log");
            var filename = path + @"\" + "LogFile_" + DateTime.Now.ToString("ddMMyyyy") + ".txt";
            System.IO.Directory.CreateDirectory(path);

            using (var file = new StreamWriter(filename, true))
            {
                file.WriteLine(message);
                file.Close();
            }
        }
    }
}
