using Microsoft.EntityFrameworkCore;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Constants;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class CustomerManagement:ICustomerManagement
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ICommonLogic _commonLogic;
        public CustomerManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            this._dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }
        public async Task<BaseResponse> InitiateCustomerSignUp(CustomerSignUpDto request)
        {
            try
            {
                //validate email
                if (!Helper.IsValidEmailAddress(request.Email))
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid email address" };
                //validate phone number

                var customer = await _dataBaseContext.Customers.Where(c => c.Email == request.Email || c.PhoneNumber == Helper.FormatPhoneNumber(request.PhoneNumber)).FirstOrDefaultAsync();
                if (customer != null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Phone Number or email already exists in the system, please reset password if you have forgotten it" };

                var otp = _commonLogic.GenerateOTP();
                var passwordDetails=Helper.EncryptPassword(request.Password);
                var newCustomer = new SignUpRequest()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    PhoneNumber = Helper.FormatPhoneNumber(request.PhoneNumber),
                    RecordDate = DateTime.Now,
                    Password = passwordDetails.Item1,
                    RandomCode=passwordDetails.Item2,
                    Status = SystemConstants.Pending,
                    CustomerType = SystemConstants.Customer,
                    CountryCode = request.CountryCode,
                    Otp = otp
                };

                var customerlog = await _dataBaseContext.AddAsync(newCustomer);
                await _dataBaseContext.SaveChangesAsync();
                if (!String.IsNullOrEmpty(request.Email))
                {
                    var emailmessage = "Hello! "+request.FirstName+" "+request.LastName +" your signup OTP is"+ otp;
                    await _commonLogic.SendEmailNotification(request.Email,emailmessage,SystemConstants.SignUpEmailSubject);
                }

                if (!String.IsNullOrEmpty(request.PhoneNumber))
                {
                    var smsmessage = "Hello! " + request.FirstName + " " + request.LastName + " your signup OTP is" + otp;
                    await _commonLogic.SendSmsNotification(Helper.FormatPhoneNumber(request.PhoneNumber), smsmessage);
                }

                return new BaseResponse()
                {
                    ResponseCode = "100",
                    ResponseMessage = "An Otp has been sent ",
                    ResponseId = customerlog.Entity.RecordId.ToString()
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseResponse> CompleteCustomerSignUp(CompleteRequestDto request)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
