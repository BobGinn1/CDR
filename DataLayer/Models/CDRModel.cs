using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class CDRModel
    {
        public int Id { get; set; }
        public string Caller_Id { get; set; }
        public string Recipient { get; set; }
        public DateTime Call_Date { get; set; }
        public DateTime End_Time { get; set; }
        public DateTime Duration { get; set; }
        public decimal Cost { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
        public int TypeId { get; set; }
    }
}
