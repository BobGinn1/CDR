using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
   public class CallCountSearchModel
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string CallerId { get; set; }
        public int Type { get; set; }

    }
}
