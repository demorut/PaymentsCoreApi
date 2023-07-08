using System;
using PaymentsCoreApi.Domain.Dtos;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface IVendorManagement
    {
        Task<BaseResponse> AddVendor(AddVendorDto request);
    }
}

