using System;
using System.Diagnostics.Metrics;
using System.Security.Principal;
using PaymentsCoreApi.Data.Contexts;
using PaymentsCoreApi.Domain.Constants;
using PaymentsCoreApi.Domain.Dtos;
using PaymentsCoreApi.Domain.Entities;
using PaymentsCoreApi.Logic.Helpers;
using PaymentsCoreApi.Logic.Interfaces;

namespace PaymentsCoreApi.Logic.Implementations
{
	public class VendorManagement : IVendorManagement
	{
        private readonly DataBaseContext _dataBaseContext;
        private readonly ICommonLogic _commonLogic;
        public VendorManagement(DataBaseContext dataBaseContext, ICommonLogic commonLogic)
        {
            this._dataBaseContext = dataBaseContext;
            _commonLogic = commonLogic;
        }

        public async Task<BaseResponse> AddVendor(AddVendorDto request)
		{
            var vendor = _dataBaseContext.Vendors.Where(v => v.VendorCode == request.VendorCode).FirstOrDefault();
            if (vendor != null)
                return new BaseResponse()
                { ResponseCode = "100", ResponseMessage = "Vendor Code already Exists in the system" };

            var country = _dataBaseContext.Country.Where(c => c.CountryCode == request.CountryCode).FirstOrDefault();
            if (country == null)
                return new BaseResponse() { ResponseCode = "100", ResponseMessage = "Selected Country is currently not supported" };

            using (var dbTran = _dataBaseContext.Database.BeginTransaction())
            {
                try
                {
                    var VendorAccount = GetVendorAccount(request,country);
                    await _dataBaseContext.Vendors.AddAsync(
                        new Vendors()
                        {
                            VendorName = request.VendorName,
                            VendorCode = request.VendorCode,
                            SystemAccountNumber = VendorAccount.AccountNumber,
                            CreatedBy = request.CreatedBy,
                        });
                    await _dataBaseContext.Account.AddAsync(VendorAccount);
                    await _dataBaseContext.SaveChangesAsync();
                    dbTran.Commit();
                    return new BaseResponse()
                    { ResponseCode = "200", ResponseMessage = "Vendor has been created successfully",};
                }
                catch (Exception)
                {
                    dbTran.Rollback();
                    throw;
                }
            }
        }

        private Account GetVendorAccount(AddVendorDto request, Country country)
        {
            var account = new Account()
            {
                CustomerId = request.VendorCode,
                AccountNumber = Helper.GetGLAccountNumber(request.VendorCode + "." + SystemConstants.SuspenseAccountcode),
                AccountName = request.VendorName,
                AccountType = SystemConstants.SuspenseAccountType,
                CurrencyCode = country.CurrencyCode,
                AccountStatus = true,
                Balance = 0,
                Approved = true,
                RecordDate=DateTime.Now,
                CreatedBy=request.CreatedBy
            };
            return account;
        }

    }
}

