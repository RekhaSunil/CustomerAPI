

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomerAPI.Model;
using Newtonsoft.Json;
using CustomerAPI.Repository.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CustomerAPI.Repository;

namespace CustomerAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : Controller
    {
        [HttpGet]
        public RetDataTable GetTransactions()
        {
            RetDataTable ret = new RetDataTable();

            try
            {
                ICustomerRepository cust = new CustomerRepository();
                ret = cust.GetTransactions();
                cust = null;
            }
            catch (Exception ex)
            {
                ret.ErrorDescription = ex.Message;
                ret.Status = false;
            }
            return ret;
        }

        [HttpGet]
        public RetDataTable GetRewards()
        {
            RetDataTable ret = new RetDataTable();

            try
            {
                ICustomerRepository cust = new CustomerRepository();
                ret = cust.GetRewards();
                cust = null;
            }
            catch (Exception ex)
            {
                ret.ErrorDescription = ex.Message;
                ret.Status = false;
            }
            return ret;
        }

        [HttpGet]
        public RetDataTable GetFinalRewards(string CustomerID)
        {
            RetDataTable ret = new RetDataTable();

            try
            {
                ICustomerRepository cust = new CustomerRepository();
                ret = cust.GetFinalRewards(CustomerID);
                cust = null;
            }
            catch (Exception ex)
            {
                ret.ErrorDescription = ex.Message;
                ret.Status = false;
            }
            return ret;
        }

       
    }
}
