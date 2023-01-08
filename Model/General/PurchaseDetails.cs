using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Model.General
{
    
    public class PurchaseDetails
    {
        public string CustomerID { get; set; }
        public string Month { get; set; }
        public double Rewards { get; set; }
        public double TotRewards { get; set; }

      }
}
