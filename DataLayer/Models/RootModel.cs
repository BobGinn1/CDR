using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
   public class RootModel
    {
        public CallCountSearchModel CallCount { get; set; }
        public MostExpensiveSearchModel MostExpensive { get; set; }
    }
}
