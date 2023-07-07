using System;
using Microsoft.Extensions.Logging;
using System.Data;
using System.IO;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;
using PaymentsCoreApi.Domain.Constants;
using System.Diagnostics.Metrics;
using static System.Net.WebRequestMethods;
using System.Xml.Linq;

using System.Xml.XPath;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class AgentManagement : IAgentManagement
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ICommonLogic _commonLogic;
        public AgentManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            this._dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }

        public async Task<BaseResponse> InitiateAgentSignUp(AgentSignUpRequestDto request)
        {
            try
            {
                var credentials = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentials == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentials.ChannelKey + credentials.ChannelSecretKey + request.IdNumber + request.PhoneNumber + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                if (!Helper.IsValidEmailAddress(request.Email))
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Invalid email address" };

                var agentrequest = new AgentSignUpRequest()
                {
                    AgentId = "",
                    AgentName = request.AgentName,
                    ContactName = request.ContactName,
                    AgentType = request.AgentType,
                    AgentStatus = SystemConstants.Pending,
                    CountryCode = request.CountryCode,
                    PhoneNumber = request.PhoneNumber,
                    Email = request.Email,
                    City = request.City,
                    Street = request.Street,
                    IdType = request.IdType,
                    IdNumber = request.IdNumber,
                    Documents = System.Text.Json.JsonSerializer.Serialize(request.Documents)
                };
                //sign up customer
                using (var dbTran = _dataBaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        var record = await _dataBaseContext.AddAsync(agentrequest);
                        await _dataBaseContext.SaveChangesAsync();
                        var agentId = Helper.GetAgentId(record.Entity.RecordId);

                        record.Entity.AgentId = agentId;
                        _dataBaseContext.AgentSignUpRequests.Attach(record.Entity);
                        _dataBaseContext.Entry(record.Entity).Property(x => x.AgentId).IsModified = true;
                        await _dataBaseContext.SaveChangesAsync();
                        dbTran.Commit();
                        return new BaseResponse() { ResponseCode = "200", ResponseMessage = "e agent profile has been created and is pending approval" };
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

        public async Task<BaseResponse> ApproveorRejectAgent(AgentApprovalRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.AgentId + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var agentrecord = _dataBaseContext.AgentSignUpRequests.Where(a => a.AgentId == request.AgentId).Where(a => a.AgentStatus == SystemConstants.Pending).FirstOrDefault();
                if (agentrecord == null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "The agent request is not in a pending State." };

                var country = _dataBaseContext.Country.Where(c=>c.CountryCode==agentrecord.CountryCode).SingleOrDefault();
                if(country==null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "Country is not supported" };

                if (request.ApprovalStatus.Equals(SystemConstants.Approved))
                {
                    await CreateAgentProfile(agentrecord, country,request);
                    return new BaseResponse() { ResponseCode = "200", ResponseMessage = "The agent application request has been approved successful" };
                }
                else
                {
                    agentrecord.AgentStatus = request.ApprovalStatus;
                    agentrecord.ApprovedDate = DateTime.Now;
                    agentrecord.ApprovedBy = request.ApprovedBy;
                    _dataBaseContext.AgentSignUpRequests.Attach(agentrecord);
                    _dataBaseContext.Entry(agentrecord).Property(x => x.AgentStatus).IsModified = true;
                    _dataBaseContext.Entry(agentrecord).Property(x => x.ApprovedDate).IsModified = true;
                    _dataBaseContext.Entry(agentrecord).Property(x => x.ApprovedBy).IsModified = true;
                    await _dataBaseContext.SaveChangesAsync();
                    return new BaseResponse() { ResponseCode = "200", ResponseMessage = "The agent application request has been rejected" };
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task CreateAgentProfile(AgentSignUpRequest agentrecord,Country country, AgentApprovalRequestDto request)
        {
            try {
                using (var dbTran = _dataBaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        var agent = new Agents()
                        {
                            AgentId = agentrecord.AgentId,
                            AgentName = agentrecord.AgentId,
                            ContactName = agentrecord.ContactName,
                            AgentType = agentrecord.AgentType,
                            AgentStatus = true,
                            CountryCode = agentrecord.CountryCode,
                            PhoneNumber = agentrecord.PhoneNumber,
                            Email = agentrecord.Email,
                            City = agentrecord.City,
                            Street = agentrecord.Street,
                            IdType = agentrecord.IdType,
                            IdNumber = agentrecord.IdNumber

                        };
                        var password = _commonLogic.GenerateOTP();
                        var apsswordvalues = Helper.EncryptPassword(password);
                        var userlogin = new UserLogins()
                        {
                            CustomerId = agent.AgentId,
                            Username = agent.AgentId,
                            Password = apsswordvalues.Item1,
                            RandomCode = apsswordvalues.Item2,
                            Reset = false,
                            RecordDate = DateTime.Now,
                            Active = true,
                            LastPasswordChangeDate = DateTime.Now,
                            LoginAttempts = 0,
                            LastLoginDate = DateTime.Now,
                            ResetPassword = true
                        };
                        var account = new Account()
                        {
                            CustomerId = agentrecord.AgentId,
                            AccountNumber = Helper.GenerateAgentAccountNumber(agentrecord.AgentId),
                            AccountName = "",
                            AccountType = SystemConstants.CustomerAccountType,
                            CurrencyCode = country.CurrencyCode,
                            AccountStatus = true,
                            Balance = 0,

                        };
                        System.Threading.Thread.Sleep(new TimeSpan(0, 0, 3));
                        var commaccount = new Account()
                        {
                            CustomerId = agentrecord.AgentId,
                            AccountNumber = Helper.GenerateAgentAccountNumber(agentrecord.AgentId),
                            AccountName = "",
                            AccountType = SystemConstants.CommissionAccountcode,
                            CurrencyCode = country.CurrencyCode,
                            AccountStatus = true,
                            Balance = 0,

                        };
                        List<UserDocumentDto> documents = System.Text.Json.JsonSerializer.Deserialize<List<UserDocumentDto>>(agentrecord.Documents);
                        foreach (var document in documents)
                        {
                            var docItem = new UserDocuments()
                            {
                                UserId=agentrecord.AgentId,
                                DocumentType=document.DocumentType,
                                DocumentName=document.DocumentName,
                                DocumentExtension=document.DocumentExtension,
                                DocumentPath=document.DocumentPath
                            };
                            await _dataBaseContext.AddAsync(agent);
                        }
                        await _dataBaseContext.AddAsync(agent);
                        await _dataBaseContext.AddAsync(userlogin);
                        await _dataBaseContext.AddAsync(account);
                        await _dataBaseContext.AddAsync(account);
                        agentrecord.AgentStatus = request.ApprovalStatus;
                        agentrecord.ApprovedDate = DateTime.Now;
                        agentrecord.ApprovedBy = request.ApprovedBy;
                        _dataBaseContext.AgentSignUpRequests.Attach(agentrecord);
                        _dataBaseContext.Entry(agentrecord).Property(x => x.AgentStatus).IsModified = true;
                        _dataBaseContext.Entry(agentrecord).Property(x => x.ApprovedDate).IsModified = true;
                        _dataBaseContext.Entry(agentrecord).Property(x => x.ApprovedBy).IsModified = true;
                        await _dataBaseContext.SaveChangesAsync();
                        dbTran.Commit();

                        if (!String.IsNullOrEmpty(agentrecord.PhoneNumber))
                        {
                            var smsmessage = "Hello! " + agentrecord.ContactName + " your agent id  is "+agentrecord.AgentId +"and your password is "+ password;
                            await _commonLogic.SendSmsNotification(Helper.FormatPhoneNumber(agentrecord.PhoneNumber), smsmessage);
                        }
                    }
                    catch (Exception)
                    {
                        dbTran.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception )
            {
                throw;
            }
        }
    }
}

