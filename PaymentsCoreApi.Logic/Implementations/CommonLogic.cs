using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class CommonLogic:ICommonLogic
    {
        private IHttpServices _httpServices;
        private DataBaseContext _dataBaseContext;
        public CommonLogic(DataBaseContext dataBaseContext, IHttpServices httpServices)
        {
            _dataBaseContext = dataBaseContext;
            _httpServices = httpServices;
        }

        public bool IsValidCredentails(string signature, string inputstring)
        {
            try
            {
                if (!String.IsNullOrEmpty(signature) && !String.IsNullOrEmpty(inputstring))
                {
                    var hash = Helper.GenerateApiSignature(inputstring);
                    if (signature.Equals(hash))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
