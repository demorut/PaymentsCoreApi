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

                var country = _dataBaseContext.Country.Where(c => c.CountryCode == request.CountryCode).FirstOrDefault();
                if(country==null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Selected Country is currently not supported" };

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
                    ResponseCode = "200",
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
                var signuprequest = await _dataBaseContext.SignUpRequest.Where(s => s.RecordId == Convert.ToInt64(request.RequestId)).FirstOrDefaultAsync();

                if (signuprequest == null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid request id" };

                if (!signuprequest.Otp.Equals(request.Otp))
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid otp" };

                if(DateTime.Now>signuprequest.RecordDate.AddMinutes(5))
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Otp has expired" };

                var country=await _dataBaseContext.Country.Where(c=>c.CountryCode==signuprequest.CountryCode).FirstOrDefaultAsync();
                if (country == null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Country is not Currently supported" };

                //sign up customer
                using (var dbTran = _dataBaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        var newCustomer = new Customers()
                        {
                            FirstName = signuprequest.FirstName,
                            LastName = signuprequest.LastName,
                            Email = signuprequest.Email,
                            PhoneNumber = signuprequest.PhoneNumber,
                            RecordDate = DateTime.Now,
                            CustomerStatus = true,
                            CustomerType = SystemConstants.Customer,
                            CountryCode = signuprequest.CountryCode
                        };
                        var userlogin = new UserLogins()
                        {
                            CustomerId= signuprequest.PhoneNumber,
                            Username= signuprequest.PhoneNumber,
                            Password=signuprequest.Password,
                            RandomCode=signuprequest.RandomCode,
                            Reset=false,
                            RecordDate= DateTime.Now,
                            Active=true,
                            LastPasswordChangeDate= DateTime.Now,
                            LoginAttempts=0,
                            LastLoginDate=DateTime.Now,
                        };
                        var account = new Account()
                        {
                            CustomerId = signuprequest.PhoneNumber,
                            AccountNumber=_commonLogic.GetAccountNumber(signuprequest.PhoneNumber),
                            AccountName="",
                            AccountType = SystemConstants.CustomerAccountType,
                            CurrencyCode= country.CurrencyCode,
                            AccountStatus=true,
                            Balance=0,

                        };
                        await _dataBaseContext.AddAsync(newCustomer);
                        await _dataBaseContext.AddAsync(userlogin);
                        await _dataBaseContext.AddAsync(account);
                        await _dataBaseContext.SaveChangesAsync();
                        dbTran.Commit();
                        return new BaseResponse() { ResponseCode = "200", ResponseMessage = "Your account has been created successfully" };
                    }
                    catch (Exception)
                    {
                        dbTran.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
