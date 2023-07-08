using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentsCoreApi.Domain.Dtos;

namespace PaymentsCoreApi.Logic.Interfaces
{
    public interface ITransactionManagement
    {
        Task<BaseResponse> MakeThridPartyDeposit(ThirdPartyDepositRequestDto request);
    }
}