using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class CDRModel
    {
        public int Id { get; set; }
        public string CallerId { get; set; }
        public string Recipient { get; set; }
        public DateTime CallDate { get; set; }
        public TimeSpan EndTime { get; set; }
        public TimeSpan Duration { get; set; }
        public decimal Cost { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
        public int TypeId { get; set; }
    }
}
