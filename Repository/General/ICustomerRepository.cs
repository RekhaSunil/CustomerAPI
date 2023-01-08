using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.Model;

namespace CustomerAPI.Repository.General
{
    public interface ICustomerRepository
    {
        RetDataTable GetTransactions();

        RetDataTable GetFinalRewards(string CustomerID);

        RetDataTable GetRewards();
    }
}
