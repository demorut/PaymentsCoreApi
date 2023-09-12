using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Ocsp;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Constants;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class UserAuthenticationManagement:IUserAuthenticationManagement
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ICommonLogic _commonLogic;
        public UserAuthenticationManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            this._dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }

        public async Task<UserDetailsDto> UserLogin(LoginRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new UserDetailsDto()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.Username +request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new UserDetailsDto()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var userlist=_dataBaseContext.UserLogins.Where(u=>u.Username== request.Username).ToList();
                if(userlist.Count!=1)
                    return new UserDetailsDto() { ResponseCode="100",ResponseMessage="Invalid User details"};

                var user=userlist.First();
                if(!user.Active)
                    return new UserDetailsDto() { ResponseCode = "100", ResponseMessage = "Your user account is deactivated please contact our customer service for assistance" };

                if (!user.Password.Equals(Helper.GenrateEncryptedPassword(request.Password, user.RandomCode)))
                {
                    if (user.LoginAttempts > 3)
                    {
                        user.LoginAttempts += 1;
                        user.Active = false;
                        _dataBaseContext.UserLogins.Attach(user);
                        _dataBaseContext.Entry(user).Property(x => x.LoginAttempts).IsModified = true;
                        _dataBaseContext.Entry(user).Property(x => x.Active).IsModified = true;
                    }
                    else
                    {
                        user.LoginAttempts += 1;
                        _dataBaseContext.UserLogins.Attach(user);
                        _dataBaseContext.Entry(user).Property(x => x.LoginAttempts).IsModified = true;
                    }
                    _dataBaseContext.SaveChanges();
                    return new UserDetailsDto() { ResponseCode = "100", ResponseMessage = "Invalid User details" };
                }

                if((DateTime.Now-user.LastPasswordChangeDate).Days>365)
                    return new UserDetailsDto() { ResponseCode = "300", ResponseMessage = "Your Password has expired please set a new Password",ResetPassword=true };


                //Successful login
                var userdetails = new UserDetailsDto()
                {
                    UserId=user.Username,
                    UserType=user.UserType,
                    ResetPassword = false,
                    ResponseCode = "200",
                    ResponseMessage = "Successful",

                };

                if (user.UserType.Equals("AGENT"))
                {
                    userdetails.AgentDetails = GetAgentDetails(request.Username);
                    userdetails.Accounts = GetAccountDetails(request.Username);
                }
                else
                {
                    userdetails.CustomerDetails = GetCustomerDetails(request.Username);
                    userdetails.Accounts = GetAccountDetails(request.Username);
                }

                return userdetails;
            }
            catch(Exception)
            {
                throw;
            }
        }

        private List<AccountDetailsDto>? GetAccountDetails(string? username)
        {
            var accounts = new List<AccountDetailsDto>();
            var accountlisting = _dataBaseContext.Account.Where(u => u.CustomerId == username).ToList();
            foreach (var account in accountlisting)
            {
                accounts.Add(new AccountDetailsDto()
                {
                    RecordId=account.RecordId.ToString(),
                    CustomerId=account.CustomerId,
                    AccountNumber=account.AccountNumber,
                    AccountName=account.AccountName,
                    AccountType=account.AccountType,
                    CurrencyCode=account.CurrencyCode,
                    AccountStatus=account.AccountStatus?"ACTIVE":"DEACTIVATED",
                    Balance=account.Balance
                }
                );
            }
            return accounts;
        }

        private CustomerDetailsDto? GetCustomerDetails(string? username)
        {
            var customerlisting = _dataBaseContext.Customers.Where(u => u.CustomerId == username).ToList();
            if (customerlisting.Count != 1)
                throw new Exception("System Can not determine user details");

            var customer = customerlisting.First();

            var customerDetails = new CustomerDetailsDto()
            {
                RecordId=customer.RecordId.ToString(),
                CustomerId=customer.CustomerId.ToString(),
                FirstName=customer.FirstName,
                LastName=customer.LastName,
                PhoneNumber=customer.PhoneNumber,
                Email=customer.Email,
                CustomerType=customer.CustomerType,
                CustomerStatus=customer.CustomerStatus,
                CountryCode=customer.CountryCode,
                UserId=customer.UserId,
            };
            return customerDetails;
        }

        private AgentDetailsDto? GetAgentDetails(string? username)
        {
            var Agentlisting = _dataBaseContext.Agents.Where(u => u.AgentId == username).ToList();
            if (Agentlisting.Count != 1)
                throw new Exception("System Can not determine user details");

            var agent = Agentlisting.First();

            var agentDetails = new AgentDetailsDto()
            {
                RecordId=agent.RecordId.ToString(),
                AgentId=agent.AgentId,
                AgentName=agent.AgentName,
                ContactName=agent.ContactName,
                AgentType=agent.AgentType,
                AgentStatus=agent.AgentStatus,
                CountryCode=agent.CountryCode,
                PhoneNumber=agent.PhoneNumber,
                Email=agent.Email,
                City=agent.City,
                Street=agent.Street,
                IdType=agent.IdType,
                IdNumber=agent.IdNumber
            };
            return agentDetails;
        }

        public async Task<UserAccountsDto> GetUserAccounts(UserAccountRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new UserAccountsDto()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.CustomerId + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new UserAccountsDto()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var userlist = _dataBaseContext.UserLogins.Where(u => u.Username == request.CustomerId).ToList();
                if (userlist.Count != 1)
                    return new UserAccountsDto() { ResponseCode = "100", ResponseMessage = "Invalid User details" };

                var user = userlist.First();
                if (!user.Active)
                    return new UserAccountsDto() { ResponseCode = "100", ResponseMessage = "Your user account is deactivated please contact our customer service for assistance" };

                var userAccounts = new UserAccountsDto()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Successful",

                };
                userAccounts.Accounts=  GetAccountDetails(request.CustomerId);
                return userAccounts;
            }
            catch (Exception ex)
            {
                return new UserAccountsDto() { ResponseCode = "100", ResponseMessage = "Failed to get user details" };
            }
        }

        public async Task<BaseResponse> ChangeUserPassword(PasswordChangeRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.Username + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var userlist = _dataBaseContext.UserLogins.Where(u => u.Username == request.Username).ToList();
                if (userlist.Count != 1)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid User details" };

                var user = userlist.First();
                if (!user.Active)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Your user account is deactivated please contact our customer service for assistance" };

                if (!user.Password.Equals(Helper.GenrateEncryptedPassword(request.OldPassword, user.RandomCode)))
                {
                    if (user.LoginAttempts > 10)
                    {
                        user.LoginAttempts += 1;
                        user.Active = false;
                        _dataBaseContext.UserLogins.Attach(user);
                        _dataBaseContext.Entry(user).Property(x => x.LoginAttempts).IsModified = true;
                        _dataBaseContext.Entry(user).Property(x => x.Active).IsModified = true;
                    }
                    else
                    {
                        user.LoginAttempts += 1;
                        _dataBaseContext.UserLogins.Attach(user);
                        _dataBaseContext.Entry(user).Property(x => x.LoginAttempts).IsModified = true;
                    }
                    _dataBaseContext.SaveChanges();
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "User Password is not valid" };
                }

                var passwordDetails = Helper.EncryptPassword(request.Password);
                user.Password = passwordDetails.Item1;
                user.RandomCode= passwordDetails.Item2;
                user.LastPasswordChangeDate= DateTime.Now;
                user.LoginAttempts = 0;
                _dataBaseContext.UserLogins.Attach(user);
                _dataBaseContext.Entry(user).Property(x => x.LoginAttempts).IsModified = true;
                _dataBaseContext.Entry(user).Property(x => x.Password).IsModified = true;
                _dataBaseContext.Entry(user).Property(x => x.RandomCode).IsModified = true;
                _dataBaseContext.Entry(user).Property(x => x.LastPasswordChangeDate).IsModified = true;
                _dataBaseContext.SaveChanges();

                return new BaseResponse()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Your password has been changed"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseResponse> InitiateUserPasswordReset(PasswordResetRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.Username + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var userlist = _dataBaseContext.UserLogins.Where(u => u.Username == Helper.FormatPhoneNumber(request.Username)).ToList();
                if (userlist.Count != 1)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid User details" };

                var user = userlist.First();
                if (!user.Active)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Your user account is deactivated please contact our customer service for assistance" };

                var customer = _dataBaseContext.Customers.Where(c => c.CustomerId == user.CustomerId).FirstOrDefault();

                var otp = _commonLogic.GenerateOTP();
                var newrequest = new PasswordResetRequests()
                {
                    Username = request.Username,
                    Otp = otp,
                    RecordDate = DateTime.Now,
                    CreatedBy = user.CustomerId,
                    RequestReference=request.RequestReference
                };
                var customerlog = await _dataBaseContext.AddAsync(newrequest);
                await _dataBaseContext.SaveChangesAsync();
                if (!String.IsNullOrEmpty(customer.Email))
                {
                    var emailmessage = "Hello! " + customer.FirstName + " " + customer.LastName + " your signup OTP is" + otp;
                    await _commonLogic.SendEmailNotification(customer.Email, emailmessage, SystemConstants.SignUpEmailSubject);
                }

                if (!String.IsNullOrEmpty(customer.PhoneNumber))
                {
                    var smsmessage = "Hello! " + customer.FirstName + " " + customer.LastName + " your signup OTP is" + otp;
                    await _commonLogic.SendSmsNotification(Helper.FormatPhoneNumber(customer.PhoneNumber), smsmessage);
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

        public async Task<BaseResponse> CompleteUserPasswordReset(CompletePasswordResetRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.Username+request.Otp + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var passwordrestrequest = await _dataBaseContext.PasswordResetRequests.Where(s => s.RecordId == Convert.ToInt64(request.RequestId)).FirstOrDefaultAsync();

                if (passwordrestrequest == null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid request id" };

                if (!passwordrestrequest.Otp.Equals(request.Otp))
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid otp" };

                if (DateTime.Now > passwordrestrequest.RecordDate.AddMinutes(1))
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Otp has expired" };

                var userlist = _dataBaseContext.UserLogins.Where(u => u.Username == passwordrestrequest.Username).ToList();
                if (userlist.Count != 1)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid User details" };

                var user = userlist.First();
                if (!user.Active)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Your user account is deactivated please contact our customer service for assistance" };

                var passwordDetails = Helper.EncryptPassword(request.Password);
                user.Password = passwordDetails.Item1;
                user.RandomCode = passwordDetails.Item2;
                user.LastPasswordChangeDate = DateTime.Now;
                user.LoginAttempts = 0;
                _dataBaseContext.UserLogins.Attach(user);
                _dataBaseContext.Entry(user).Property(x => x.LoginAttempts).IsModified = true;
                _dataBaseContext.Entry(user).Property(x => x.Password).IsModified = true;
                _dataBaseContext.Entry(user).Property(x => x.RandomCode).IsModified = true;
                _dataBaseContext.Entry(user).Property(x => x.LastPasswordChangeDate).IsModified = true;
                _dataBaseContext.SaveChanges();

                return new BaseResponse()
                {
                    ResponseCode = "200",
                    ResponseMessage = "Your password has been changed"
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
