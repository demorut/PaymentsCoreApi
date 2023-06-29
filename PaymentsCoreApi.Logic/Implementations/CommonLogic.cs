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

        public string GenerateOTP()
        {
            // Create a new random number generator.
            Random random = new Random();
            // Generate a 5 digit random number.
            int otp = random.Next(100000, 999999);
            // Return the OTP as a string.
            return otp.ToString();
        }
        public async Task SendEmailNotification(string email, string emailmessage, string signUpEmailSubject)
        {
            try
            {

            }
            catch (Exception ex)
            {
                // do nothing
            }
        }

        public async Task SendSmsNotification(string phonenumber, string smsmessage)
        {
            try
            {

            }
            catch (Exception ex) 
            {
                //do nothing
            }
        }
    }
}
