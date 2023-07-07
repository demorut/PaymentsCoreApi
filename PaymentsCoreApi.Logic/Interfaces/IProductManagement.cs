using System;
using PaymentsCoreApi.Domain.Dtos;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface IProductManagement
    {
        Task<BaseResponse> AddProductDetails(AddProductRequestDto request);
    }
}

