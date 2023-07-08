using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Constants;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class TransactionManagement:ITransactionManagement
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ICommonLogic _commonLogic;
        public TransactionManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            this._dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }
        public async Task<BaseResponse> MakeThridPartyDeposit(ThirdPartyDepositRequestDto request)
        {
            try
            {
                var credentials = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentials == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentials.ChannelKey + credentials.ChannelSecretKey + request.CustomerId + request.Amount + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var account = _dataBaseContext.Account.Where(a => a.CustomerId == request.CustomerId).Where(a => a.AccountNumber == request.CustomerAccount).FirstOrDefault();
                if (account == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "Customer Account Does not exist" };

                var vendor = _dataBaseContext.Vendors.Where(v => v.VendorCode == request.Network).FirstOrDefault();
                if (vendor == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "ThirdParty Vendor is not supported" };

                var deposit = new ThirdPartyDeposits()
                {
                    CustomerId = request.CustomerId,
                    CustomerAccount = request.CustomerAccount,
                    TransactionAmount = Convert.ToDouble(request.Amount),
                    RequestId = request.TransactionId,
                    Network = request.Network,
                    Channel = request.Channel,
                    Status = SystemConstants.Pending,
                    SystemId = Helper.GenerateTransactionId(),
                    RecordDate=DateTime.Now
                };

                await _dataBaseContext.AddAsync(deposit);
                await _dataBaseContext.SaveChangesAsync();
                return new BaseResponse() { ResponseCode = "200", ResponseMessage = "ThirdParty Vendor is not supported",ResponseId=deposit.SystemId };
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}