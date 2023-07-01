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
                    ResetPassword = false,
                    ResponseCode = "200",
                    ResponseMessage = "Successful",

                };
                return userdetails;
            }
            catch(Exception ex)
            {
                throw ex;
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

                var userlist = _dataBaseContext.UserLogins.Where(u => u.Username == request.Username).ToList();
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
