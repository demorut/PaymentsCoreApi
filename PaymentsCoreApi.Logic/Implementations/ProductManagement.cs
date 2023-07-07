using System;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Constants;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Logic.Implementations
{
    public class ProductManagement : IProductManagement
    {
        private readonly DataBaseContext _dataBaseContext;
        private readonly ICommonLogic _commonLogic;
        public ProductManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            this._dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }

        public async Task<BaseResponse> AddProductDetails(AddProductRequestDto request)
        {
            try
            {
                var credentails = _dataBaseContext.Channel.Where(c => c.ChannelKey == request.Apikey).FirstOrDefault();
                if (credentails == null)
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var inputstring = credentails.ChannelKey + credentails.ChannelSecretKey + request.Product.ProductCode + request.RequestTimestamp;
                if (!_commonLogic.IsValidCredentails(request.Signature, inputstring))
                    return new BaseResponse()
                    { ResponseCode = "100", ResponseMessage = "System access denied" };

                var product=  _dataBaseContext.Products.Where(c => c.ProductCode==request.Product.ProductCode).FirstOrDefault();
                if (product != null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Product already exists in the system" };

                var country = _dataBaseContext.Country.Where(c => c.CountryCode == request.Product.CountryCode).FirstOrDefault();
                if (country == null)
                    return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Country is not Supported" };

                using (var dbTran = _dataBaseContext.Database.BeginTransaction())
                {
                    try
                    {
                        var Glaccount = Helper.GetGLAccountNumber(request.Product.ProductCode+"."+SystemConstants.SuspenseAccountcode);
                        var Glaccountcomm= Helper.GetGLAccountNumber(request.Product.ProductCode+"."+SystemConstants.CommissionAccountcode);
                        var newproduct = new Products()
                        {
                            ProductCode = request.Product.ProductCode,
                            ProductName = request.Product.ProductName,
                            RecordDate = DateTime.Now,
                            Active = true,
                            CountryCode = request.Product.CountryCode,
                            Approved = true,
                            ApprovedDate = DateTime.Now,
                            ApprovedBy = "Auto",
                            SuspenseAccount=Glaccount,
                            CommissionAccount=Glaccountcomm
                        };
                        var account = new Account()
                        {
                            CustomerId = request.Product.ProductCode,
                            AccountNumber = Glaccount,
                            AccountName = request.Product.ProductName,
                            AccountType = SystemConstants.SuspenseAccountType,
                            CurrencyCode = country.CurrencyCode,
                            AccountStatus = true,
                            Balance = 0,
                            Approved = true,
                        };
                        var commaccount = new Account()
                        {
                            CustomerId = request.Product.ProductCode,
                            AccountNumber = Glaccountcomm,
                            AccountName = request.Product.ProductName,
                            AccountType = SystemConstants.SuspenseAccountType,
                            CurrencyCode = country.CurrencyCode,
                            AccountStatus = true,
                            Balance = 0,
                            Approved = true

                        };
                        await _dataBaseContext.AddAsync(newproduct);
                        await _dataBaseContext.AddAsync(account);
                        await _dataBaseContext.AddAsync(commaccount);
                        await _dataBaseContext.SaveChangesAsync();
                        dbTran.Commit();
                        return new BaseResponse() { ResponseCode = "200", ResponseMessage = "Product has been created successfully" };
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

